using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class CSharpDeveloperQuestionnaireParser : ParserStrategy
    {
        private const string questionnaireName = "Анкета си шарп разработчика";

        private const int DateOfBirthRow = 2;
        private const int DateOfBirthColumn = 1;

        private const int FullNameRow = 1;
        private const int FullNameColumn = 1;

        private const int AddressRow = 2;
        private const int AddressColumn = 2;

        private const int TelephoneNumberRow = 3;
        private const int TelephoneNumberColumn = 1;

        private const int MaritalStatusRow = 4;
        private const int MaritalStatusColumn = 1;

        public CSharpDeveloperQuestionnaireParser(WebAppOptions options) : base(options)
        {
        }

        public sealed override async Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;
                foreach (var candidateDataTable in body.ChildElements.Where(e => e.LocalName == "tbl"))
                {
                    await ParseCandidate(candidateDataTable);
                    foreach (var cell in candidateDataTable.ExtractCellsFromTable())
                    {
                        var questionsTable = cell.FirstOrDefault(c => c.LocalName == "tbl");
                        if (questionsTable != null)
                        {
                            await ParseQuestionnaire(questionsTable);
                        }
                    }
                }
            }
            return parsedData;
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ExtractRowsFromTable(false);
            var vacancyName = rows.ElementAt(0).InnerText;
            parsedData.Candidate = new Candidate
            {
                FullName = rows.ExtractCellTextFromRow(FullNameRow, FullNameColumn),
                DateOfBirth = rows.TryExtractDate(DateOfBirthRow, DateOfBirthColumn),
                Address = rows.ExtractTextAfterCharacterFromRow(AddressRow, AddressColumn, ':'),
                TelephoneNumber = rows.ExtractCellTextFromRow(TelephoneNumberRow, TelephoneNumberColumn),
                MaritalStatus = rows.ExtractCellTextFromRow(MaritalStatusRow, MaritalStatusColumn),
            };
            await VacancyParse(vacancyName);
        }

        private Task VacancyParse(string vacancyName)
        {
            vacancyName = vacancyName[(vacancyName.IndexOf(':') + 1)..];
            vacancyName = vacancyName.Trim(' ');
            parsedData.Vacancy = new Vacancy() { Name = vacancyName };
            return Task.CompletedTask;
        }

        private async Task ParseQuestionnaire(OpenXmlElement table)
        {
            parsedData.Questionnaire = new Questionnaire
            {
                Name = questionnaireName,
                VacancyId = parsedData.Vacancy.Id
            };

            foreach (var row in table.ExtractRowsFromTable())
            {
                await ParseQuestionCategory(row);
                await ParseQuestion(row);
            }
        }

        private Task ParseQuestionCategory(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 3)
            {
                currentCategory = new QuestionCategory()
                {
                    Name = child.ChildElements.ElementAt(2).InnerText,
                    Questionnaire = parsedData.Questionnaire
                };
                parsedData.QuestionCategories.Add(currentCategory);
            }
            return Task.CompletedTask;
        }

        private async Task ParseQuestion(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 5)
            {
                currentQuestion = new Question
                {
                    QuestionCategory = currentCategory,
                    Name = child.ChildElements[2].InnerText
                };
                parsedData.Questions.Add(currentQuestion);
                await ParseAnswer(child);
            }
        }

        private Task ParseAnswer(OpenXmlElement child)
        {
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                var answer = new Answer
                {
                    Candidate = parsedData.Candidate,
                    Question = currentQuestion,
                    Text = child.ChildElements[4].InnerText
                };
                _ = byte.TryParse(child.ChildElements[3].InnerText, out byte estimation);
                answer.Estimation = estimation;
                parsedData.Answers.Add(answer);
            }
            return Task.CompletedTask;
        }
    }
}

