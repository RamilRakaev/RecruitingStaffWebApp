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
    public class PhpDeveloperQuestionnaireParser : ParserStrategy
    {
        private const string questionnaireName = "Анкета PHP Junior разработчика";

        private const int DateOfBirthRow = 2;
        private const int DateOfBirthColumn = 1;

        private const int FullNameRow = 1;
        private const int FullNameColumn = 1;

        private const int TelephoneNumberRow = 3;
        private const int TelephoneNumberColumn = 1;

        private const int MaritalStatusRow = 2;
        private const int MaritalStatusColumn = 3;

        private const int EmailAddressRow = 3;
        private const int EmailAddressColumn = 3;

        private const int EducationNameRow = 5;
        private const int EducationNameColumn = 1;

        private const int DateOfStartTrainingRow = 6;
        private const int DateOfStartTrainingColumn = 1;

        private const int DateOfEndTrainingRow = 6;
        private const int DateOfEndTrainingColumn = 2;

        private const int specificationAndQualificationRow = 5;
        private const int specificationAndQualificationColumn = 2;

        public PhpDeveloperQuestionnaireParser(WebAppOptions options) : base(options)
        {
        }

        public sealed override async Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var table = body.ChildElements.Where(e => e.LocalName == "tbl").First();

                await ParseCandidate(table);
                await CreateQuestionnaire();
                var questionCategories = table.Last().Where(e => e.LocalName == "tc").First();
                await ParseQuestionCategory(questionCategories);
            }
            return parsedData;
        }

        private Task CreateQuestionnaire()
        {
            parsedData.AddQuestionnaire(new Questionnaire
            {
                Name = questionnaireName,
                Vacancy = parsedData.Vacancy,
            });
            return Task.CompletedTask;
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ExtractRowsFromTable(false);
            var vacancyName = rows.ElementAt(0).InnerText;

            parsedData.Candidate = new Candidate
            {
                FullName = rows.ExtractCellTextFromRow(FullNameRow, FullNameColumn),
                DateOfBirth = rows.TryExtractDate(DateOfBirthRow, DateOfBirthColumn),
                TelephoneNumber = rows.ExtractCellTextFromRow(TelephoneNumberRow, TelephoneNumberColumn),
                MaritalStatus = rows.ExtractCellTextFromRow(MaritalStatusRow, MaritalStatusColumn),
                EmailAddress = rows.ExtractCellTextFromRow(EmailAddressRow, EmailAddressColumn),
            };
            parsedData.Candidate.Educations = new();
            EducationParse(rows, parsedData.Candidate);
            await VacancyParse(vacancyName);
        }

        private void EducationParse(IEnumerable<OpenXmlElement> rows, Candidate candidate)
        {
            var education = new Education
            {
                Candidate = candidate,
                EducationalInstitutionName = rows.ExtractCellTextFromRow(EducationNameRow, EducationNameColumn)
            };
            var specificationAndQualification =
                rows.ExtractCellTextFromRow(
                specificationAndQualificationRow,
                specificationAndQualificationColumn).Split(",");
            education.Specialization = specificationAndQualification[0].Trim(' ');
            education.Qualification = specificationAndQualification[1].Trim(' ');
            education.StartDateOfTraining = rows.TryExtractDate(DateOfStartTrainingRow, DateOfStartTrainingColumn);
            education.EndDateOfTraining = rows.TryExtractDate(DateOfEndTrainingRow, DateOfEndTrainingColumn);
            parsedData.Candidate.Educations.Add(education);
        }

        private Task VacancyParse(string vacancyName)
        {
            parsedData.Vacancy = new Vacancy() { Name = vacancyName.GetTextAfterCharacter(':') };
            return Task.CompletedTask;
        }

        private async Task ParseQuestionCategory(OpenXmlElement tables)
        {
            foreach (var elements in tables.ChildElements
                .Where(e => e.LocalName == "tbl" ||
                           (e.LocalName == "p" &&
                           e.InnerText != " " &&
                           e.InnerText != ""))
                .Skip(1))
            {
                if (elements.LocalName == "p")
                {
                    await parsedData.AddQuestionCategory(new() { Name = elements.InnerText });
                }
                else if (elements.LocalName == "tbl")
                {
                    foreach (var row in elements.ExtractRowsFromTable())
                    {
                        await ParseQuestion(row);
                    }
                }
            }
        }

        private async Task ParseQuestion(OpenXmlElement row)
        {
            var cells = row.ExtractCellsFromRow();
            await parsedData.AddQuestion(new Question
            {
                Name = cells.ElementAt(1).InnerText
            });
            await ParseAnswer(cells);

        }

        private Task ParseAnswer(IEnumerable<OpenXmlElement> cells)
        {
            parsedData.AddAnswer(new Answer
            {
                Candidate = parsedData.Candidate,
                Question = currentQuestion,
                Text = cells.ElementAt(3).InnerText
            });
            return Task.CompletedTask;
        }
    }
}
