using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.Options;
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
        private readonly QuestionnaireDbManager questionnaireDbManager;

        public QuestionnaireManager(IOptions<WebAppOptions> options, IMediator mediator)
        {
            _options = options.Value;
            questionnaireDbManager = new QuestionnaireDbManager(mediator);
        }

        public List<string> Errors { get; private set; } = new List<string>();

        public async Task<bool> ParseAndSaveQuestionnaireExampleAsync(ParseParameters parseParameters)
        {
            var parserStrategy = FindingSuitableParserType.FindParserByFile(parseParameters.Path, parseParameters.ContentType);
            try
            {
                var parsedData = await parserStrategy.ParseAsync(parseParameters.Path);
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await Save(parsedData, parseParameters.Path, false);
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

        public async Task<bool> ParseAndSaveCompletedQuestionnaireAsync(ParseParameters parseParameters)
        {
            try
            {
                var parsedData = await ParseCompletedQuestionnaire(parseParameters);
                var checking = new ParsedDataCheck(Array.Empty<string>());
                if (checking.Checking(parsedData))
                {
                    await Save(parsedData, parseParameters.Path, true);
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

        private static async Task<ParsedData> ParseCompletedQuestionnaire(ParseParameters parseParameters)
        {
            ParserStrategy parserStrategy;
            if (parseParameters.QuestionnaireId == 0)
            {
                parserStrategy = FindingSuitableParserType.FindParserByFile(parseParameters.Path, parseParameters.ContentType);
            }
            else
            {
                parserStrategy = FindingSuitableParserType.ParserSubstitution(parseParameters.JobQuestionnaire);
            }
            var parsedData = await parserStrategy.ParseAsync(parseParameters.Path);
            parsedData.CandidateId = parseParameters.CandidateId;
            parsedData.QuestionnaireId = parseParameters.QuestionnaireId;
            return parsedData;
        }

        private async Task Save(ParsedData parsedData, string oldPath, bool isCompletedQuestionnaire)
        {
            await questionnaireDbManager.SaveParsedDataInDb(parsedData, isCompletedQuestionnaire);
            string newPath;
            if (isCompletedQuestionnaire)
            {
                newPath = Path.Combine(_options.CompletedQuestionnaireSource, parsedData.FileSource);
            }
            else
            {
                newPath = Path.Combine(_options.QuestionnaireExampleSource, parsedData.FileSource);
            }
            File.Delete(newPath);
            File.Copy(oldPath, newPath);
        }
    }
}