using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
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

        private string _fileName;

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

        public async Task<bool> Parse(string fileName, JobQuestionnaire jobQuestionnaire, bool parseQuestions)
        {
            _fileName = fileName;
            var oldPath = $"{_options.DocumentsSource}\\{_fileName}";
            await Parsersearch(jobQuestionnaire);
            try
            {
                parsedData = await parserStrategy.Parse(fileName);
                var checking = new ParsedDataCheck(new string[] { "FullName" });
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, parseQuestions);
                    var newPath = $"{_options.DocumentsSource}\\{questionnaireDbManager.File.Source}";
                    File.Delete(newPath);
                    File.Copy(oldPath, newPath);
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
                File.Delete(oldPath);
            }
        }

        private Task Parsersearch(JobQuestionnaire jobQuestionnaire)
        {
            if (jobQuestionnaire == JobQuestionnaire.CSharpDeveloperQuestionnaire)
            {
                parserStrategy = new CSharpDeveloperQuestionnaireParser(_options);
            }
            if (jobQuestionnaire == JobQuestionnaire.PhpDeveloperQuestionnaire)
            {
                parserStrategy = new PhpDeveloperQuestionnaireParser(_options);
            }
            if (jobQuestionnaire == JobQuestionnaire.OfficeQuestionnaire)
            {
                parserStrategy = new OfficeQuestionnaireParser(_options);
            }
            if (jobQuestionnaire == JobQuestionnaire.DevOpsQuestionnaire)
            {
                parserStrategy = new DevOpsQuestionnaireParser(_options);
            }
            return Task.CompletedTask;
        }
    }
}