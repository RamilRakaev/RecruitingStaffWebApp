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

        public async Task<bool> ParseAndSaveQuestions(string fileName, JobQuestionnaire jobQuestionnaire)
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
                    await questionnaireDbManager.SaveParsedData(parsedData);
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

        private async Task<bool> ParseAndSaveQuestionsFunc(string oldPath)
        {
            parsedData = await parserStrategy.Parse(oldPath);
            var checking = new ParsedDataCheck(new string[] { "FullName" });
            if (checking.Checking(parsedData))
            {
                await questionnaireDbManager.SaveParsedData(parsedData, true);
                var newPath = $"{_options.DocumentsSource}\\{questionnaireDbManager.File.Source}";
                File.Delete(newPath);
                File.Copy(oldPath, newPath);
                return true;
            }
            else
            {
                Errors.AddRange(checking.ExceptionMessages);
                return false;
            }
        }

        public async Task<bool> ParseAndSaveAnswers(string fileName, JobQuestionnaire jobQuestionnaire)
        {
            return await Parse(fileName, jobQuestionnaire, ParseAndSaveAnswersFunc);
        }
        
        private async Task<bool> ParseAndSaveAnswersFunc(string oldPath)
        {
            parsedData = await parserStrategy.Parse(oldPath);
            var checking = new ParsedDataCheck(new string[] { "FullName" });
            if (checking.Checking(parsedData))
            {
                await questionnaireDbManager.SaveParsedData(parsedData, true);
                var newPath = $"{_options.DocumentsSource}\\{questionnaireDbManager.File.Source}";
                File.Delete(newPath);
                File.Copy(oldPath, newPath);
                return true;
            }
            else
            {
                Errors.AddRange(checking.ExceptionMessages);
                return false;
            }
        }

        public async Task<bool> Parse(string fileName, JobQuestionnaire jobQuestionnaire, Func<string, Task<bool>> parseFunc)
        {
            _fileName = fileName;
            var oldPath = $"{_options.DocumentsSource}\\{_fileName}";
            await Parsersearch(jobQuestionnaire);
            try
            {
                return await parseFunc(oldPath);
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
            return Task.CompletedTask;
        }
    }
}