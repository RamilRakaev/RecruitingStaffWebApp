using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireManager : IQuestionnaireManager
    {
        private readonly WebAppOptions _options;

        private string _path;

        private ParsedData parsedData;
        private readonly QuestionnaireDbManager questionnaireDbManager;
        private ParserStrategy parserStrategy;

        public QuestionnaireManager(
            IOptions<WebAppOptions> options,
            IMediator mediator)
        {
            _options = options.Value;
            questionnaireDbManager = new QuestionnaireDbManager(mediator);
        }

        public List<string> Errors { get; private set; } = new List<string>();

        public async Task<bool> ParseQuestionnaire(string path, JobQuestionnaire jobQuestionnaire)
        {
            _path = path;
            await Parsersearch(jobQuestionnaire);
            try
            {
                parsedData = await parserStrategy.Parse(path);
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, true);
                    var newPath = $"{path[..path.LastIndexOf('\\')]}{parsedData.FileSource}";
                    File.Delete(newPath);
                    File.Copy(path, newPath);
                }
                else
                {
                    Errors.AddRange(checking.ExceptionMessages);
                }
                return true;
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                return false;
            }
            finally
            {
                File.Delete(path);
            }
        }

        public async Task<bool> ParseAnswersAndCandidateData(string path, JobQuestionnaire jobQuestionnaire, int candidateId)
        {
            await Parsersearch(jobQuestionnaire);
            try
            {
                parsedData = await parserStrategy.Parse(path);
                parsedData.CandidateId = candidateId;
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, false);
                    var newPath = $"{_options.CandidateDocumentsSource}\\{parsedData.FileSource}";
                    File.Delete(newPath);
                    File.Copy(path, newPath);
                }
                else
                {
                    Errors.AddRange(checking.ExceptionMessages);
                }
                return true;
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                return false;
            }
            finally
            {
                File.Delete(path);
            }
        }

        private Task Parsersearch(JobQuestionnaire jobQuestionnaire)
        {
            if (jobQuestionnaire == JobQuestionnaire.CSharpDeveloperQuestionnaire)
            {
                parserStrategy = new CSharpDeveloperQuestionnaireParser();
            }
            if (jobQuestionnaire == JobQuestionnaire.PhpDeveloperQuestionnaire)
            {
                parserStrategy = new PhpDeveloperQuestionnaireParser();
            }
            if (jobQuestionnaire == JobQuestionnaire.OfficeQuestionnaire)
            {
                parserStrategy = new OfficeQuestionnaireParser();
            }
            if (jobQuestionnaire == JobQuestionnaire.DevOpsQuestionnaire)
            {
                parserStrategy = new DevOpsQuestionnaireParser();
            }
            return Task.CompletedTask;
        }
    }
}