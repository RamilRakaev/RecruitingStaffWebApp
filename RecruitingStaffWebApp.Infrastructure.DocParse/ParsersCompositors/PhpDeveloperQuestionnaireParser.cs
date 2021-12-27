using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Model;
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

        public PhpDeveloperQuestionnaireParser()
        {
        }

        public sealed override Task<ParsedData> Parse(string path)
        {
            using var wordDoc = WordprocessingDocument.Open(path, false);
            var body = wordDoc.MainDocumentPart.Document.Body;

            var table = body.ChildElements.Where(e => e.LocalName == "tbl").First();

            return Task.FromResult(ParseData(table));
        }

        private ParsedData ParseData(OpenXmlElement table)
        {
            ParsedData parsedData = new();
            var rows = table.ExtractRowsFromTable(false);
            var vacancyName = rows.ElementAt(0).InnerText;

            parsedData.Candidate = ParseCandidate(rows);
            parsedData.Vacancy = VacancyParse(vacancyName);
            var questionCategories = table.Last().Where(e => e.LocalName == "tc").First();
            parsedData.Questionnaire = ParseQuestionnaire(questionCategories);
            return parsedData;
        }

        private Candidate ParseCandidate(IEnumerable<OpenXmlElement> rows)
        {
            Candidate candidate = new()
            {
                FullName = rows.ExtractCellTextFromRow(FullNameRow, FullNameColumn),
                DateOfBirth = rows.TryExtractDate(DateOfBirthRow, DateOfBirthColumn),
                TelephoneNumber = rows.ExtractCellTextFromRow(TelephoneNumberRow, TelephoneNumberColumn),
                MaritalStatus = rows.ExtractCellTextFromRow(MaritalStatusRow, MaritalStatusColumn),
                EmailAddress = rows.ExtractCellTextFromRow(EmailAddressRow, EmailAddressColumn),
            };
            candidate.Educations.Add(EducationParse(rows));
            return candidate;
        }

        private Education EducationParse(IEnumerable<OpenXmlElement> rows)
        {
            var education = new Education
            {
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
            return education;
        }

        private Vacancy VacancyParse(string vacancyName)
        {
            return new Vacancy() { Name = vacancyName.GetTextAfterCharacter(':') };
        }

        private QuestionnaireElement ParseQuestionnaire(OpenXmlElement tables)
        {
            QuestionnaireElement currentQuestionnaire = new(questionnaireName);
            foreach (var elements in tables.ChildElements
                .Where(e => e.LocalName == "tbl" ||
                           (e.LocalName == "p" &&
                           e.InnerText != " " &&
                           e.InnerText != ""))
                .Skip(1))
            {
                if (elements.LocalName == "p")
                {
                    var currentQuestionCategory = QuestionnaireElement.CreateQuestionnaireElement(elements.InnerText);
                    currentQuestionnaire.AddChildElement(currentQuestionCategory);
                }
                else if (elements.LocalName == "tbl")
                {
                    foreach (var row in elements.ExtractRowsFromTable())
                    {
                        currentQuestionnaire.CurrentChildElement.AddChildElement(ParseQuestion(row));
                    }
                }
            }
            return currentQuestionnaire;
        }

        private QuestionnaireElement ParseQuestion(OpenXmlElement row)
        {
            var cells = row.ExtractCellsFromRow();
            var question = QuestionnaireElement.CreateQuestionnaireElement(
                            cells.ElementAt(1).InnerText);
            question.AddChildElement(ParseAnswer(cells));
            return question;
        }

        private QuestionnaireElement ParseAnswer(IEnumerable<OpenXmlElement> cells)
        {
            Dictionary<string, string> properties = new()
            {
                { "Name", cells.ElementAt(3).InnerText },
                { "FamiliarWithTheTechnology", cells.ElementAt(2).InnerText }
            };
            return QuestionnaireElement.CreateQuestionnaireElement(properties);
        }
    }
}
