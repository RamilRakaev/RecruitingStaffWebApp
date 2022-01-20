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

        public QuestionnaireManager(IOptions<WebAppOptions> options, IMediator mediator)
        {
            _options = options.Value;
            questionnaireDbManager = new QuestionnaireDbManager(mediator);
        }

        public List<string> Errors { get; private set; } = new List<string>();

        public async Task<bool> ParseQuestionnaireExampleAsync(ParseParameters parseParameters)
        {
            var parserStrategy = ParserSearch(parseParameters.JobQuestionnaire);
            try
            {
                var parsedData = await parserStrategy.ParseAsync(parseParameters.Path);
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, true);
                    var newPath = Path.Combine(_options.EmptyQuestionnairesSource, parsedData.FileSource);
                    File.Delete(newPath);
                    File.Copy(parseParameters.Path, newPath);
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
                File.Delete(parseParameters.Path);
            }
        }

        public async Task<bool> ParseCompletedQuestionnaireAsync(ParseParameters parseParameters)
        {
            var parserStrategy = ParserSearch(parseParameters.JobQuestionnaire);
            try
            {
                var parsedData = await parserStrategy.ParseAsync(parseParameters.Path);
                parsedData.CandidateId = parseParameters.CandidateId;
                parsedData.QuestionnaireId = parseParameters.QuestionnaireId;
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await questionnaireDbManager.SaveParsedData(parsedData, false);
                    var newPath = Path.Combine(_options.CandidateDocumentsSource, parsedData.FileSource);
                    File.Delete(newPath);
                    File.Copy(parseParameters.Path, newPath);
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
                File.Delete(parseParameters.Path);
            }
        }

        private static ParserStrategy ParserSearch(JobQuestionnaireType jobQuestionnaire)
        {
            return jobQuestionnaire switch
            {
                JobQuestionnaireType.CSharpDeveloperQuestionnaire =>
                     new CSharpDeveloperQuestionnaireParser(),
                JobQuestionnaireType.PhpDeveloperQuestionnaire =>
                    new PhpDeveloperQuestionnaireParser(),
                JobQuestionnaireType.OfficeQuestionnaire =>
                    new OfficeQuestionnaireParser(),
                JobQuestionnaireType.DevOpsQuestionnaire =>
                    new DevOpsQuestionnaireParser(),
                _ => throw new ParseException()
            };
        }
    }
}