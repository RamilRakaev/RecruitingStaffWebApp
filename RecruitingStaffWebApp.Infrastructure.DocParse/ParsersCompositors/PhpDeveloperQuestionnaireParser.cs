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
        private const int DateOfBirthRow = 2;
        private const int DateOfBirthColumn = 1;

        private const int FullNameRow = 1;
        private const int FullNameColumn = 1;

        private const int AddressRow = 2;
        private const int AddressColumn = 2;

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
                questionnaireName = body.ChildElements.Where(e => e.LocalName == "p").FirstOrDefault().InnerText;


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
            parsedData.Questionnaire = new Questionnaire
            {
                Name = questionnaireName,
                Vacancy = parsedData.Vacancy
            };
            return Task.CompletedTask;
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ChildElements.Where(e => e.LocalName == "tr");
            var vacancyName = rows.ElementAt(0).InnerText;

            parsedData.Candidate = new Candidate
            {
                FullName = ExtractCellTextFromRow(rows, FullNameRow, FullNameColumn)
            };
            var dateStr = ExtractCellTextFromRow(rows, DateOfBirthRow, DateOfBirthColumn);
            _ = DateTime.TryParse(dateStr, out var DateOfBirth);
            parsedData.Candidate.DateOfBirth = DateOfBirth;
            parsedData.Candidate.TelephoneNumber = ExtractCellTextFromRow(rows, TelephoneNumberRow, TelephoneNumberColumn);
            parsedData.Candidate.MaritalStatus = ExtractCellTextFromRow(rows, MaritalStatusRow, MaritalStatusColumn);
            parsedData.Candidate.EmailAddress = ExtractCellTextFromRow(rows, EmailAddressRow, EmailAddressColumn);
            EducationParse(rows, parsedData.Candidate);
            await VacancyParse(vacancyName);
        }

        private void EducationParse(IEnumerable<OpenXmlElement> row, Candidate candidate)
        {
            var education = new Education
            {
                Candidate = candidate,
                EducationalInstitutionName = ExtractCellTextFromRow(row, EducationNameRow, EducationNameColumn)
            };
            var specificationAndQualification =
                ExtractCellTextFromRow(row,
                specificationAndQualificationRow,
                specificationAndQualificationColumn).Split(",");
            education.Specification = specificationAndQualification[0].Trim(' ');
            education.Qualification = specificationAndQualification[1].Trim(' ');
            //var dates = ExtractCellTextFromRow(table, DateOfEndTrainingRow, DateOfTrainingColumn).Split("\\");
            var dateOfStart = ExtractCellTextFromRow(row, DateOfStartTrainingRow, DateOfStartTrainingColumn);
            var dateOfEnd = ExtractCellTextFromRow(row, DateOfEndTrainingRow, DateOfEndTrainingColumn);
            //_ = DateTime.TryParse(dates[0], out var startDateOfTraining);
            //_ = DateTime.TryParse(dates[1], out var endDateOfTraining);
            _ = DateTime.TryParse(dateOfStart, out var startDateOfTraining);
            _ = DateTime.TryParse(dateOfEnd, out var endDateOfTraining);
            education.StartDateOfTraining = startDateOfTraining;
            education.EndDateOfTraining = endDateOfTraining;
            parsedData.Educations.Add(education);
        }

        private Task VacancyParse(string vacancyName)
        {
            vacancyName = vacancyName[(vacancyName.IndexOf(':') + 1)..];
            vacancyName = vacancyName.Trim(' ');
            parsedData.Vacancy = new Vacancy() { Name = vacancyName };
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
                    currentCategory = new();
                    currentCategory.Name = elements.InnerText;
                    parsedData.QuestionCategories.Add(currentCategory);
                }
                else if (elements.LocalName == "tbl")
                {
                    foreach (var row in elements.ChildElements.Where(e => e.LocalName == "tr").Skip(1))
                    {
                        await ParseQuestion(row);
                    }
                }
            }
        }

        private async Task ParseQuestion(OpenXmlElement row)
        {
            var cells = row.ChildElements.Where(e => e.LocalName == "tc");
            currentQuestion = new Question
            {
                QuestionCategory = currentCategory,
                Name = cells.ElementAt(1).InnerText
            };
            parsedData.Questions.Add(currentQuestion);
            await ParseAnswer(cells);

        }

        private Task ParseAnswer(IEnumerable<OpenXmlElement> cells)
        {
            var answer = new Answer
            {
                Candidate = parsedData.Candidate,
                Question = currentQuestion,
                Text = cells.ElementAt(3).InnerText
            };
            parsedData.Answers.Add(answer);
            return Task.CompletedTask;
        }
    }
}

