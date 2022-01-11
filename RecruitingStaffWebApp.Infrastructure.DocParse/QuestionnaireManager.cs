using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireManager : IQuestionnaireManager
    {
        private readonly WebAppOptions _options;

        private readonly QuestionnaireDbManager questionnaireDbManager;

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
            var parserStrategy = Parsersearch(jobQuestionnaire);
            try
            {
                var parsedData = await parserStrategy.ParseAsync(path);
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, true);
                    var newPath = Path.Combine(_options.EmptyQuestionnairesSource, parsedData.FileSource);
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
            var parserStrategy = Parsersearch(jobQuestionnaire);
            try
            {
                var parsedData = await parserStrategy.ParseAsync(path);
                parsedData.CandidateId = candidateId;
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, false);
                    var newPath = Path.Combine(_options.CandidateDocumentsSource, parsedData.FileSource);
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

        private ParserStrategy Parsersearch(JobQuestionnaire jobQuestionnaire)
        {
            return jobQuestionnaire switch
            {
                JobQuestionnaire.CSharpDeveloperQuestionnaire =>
                     new CSharpDeveloperQuestionnaireParser(),
                JobQuestionnaire.PhpDeveloperQuestionnaire =>
                    new PhpDeveloperQuestionnaireParser(),
                JobQuestionnaire.OfficeQuestionnaire =>
                    new OfficeQuestionnaireParser(),
                JobQuestionnaire.DevOpsQuestionnaire =>
                    new DevOpsQuestionnaireParser(),
                _ => throw new ParseException()
            };
        }
    }
}