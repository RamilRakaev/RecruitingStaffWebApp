using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Exceptions;
using RecruitingStaffWebApp.Services.DocParse.Model;
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

        private const int EmailRow = 3;
        private const int EmailColumn = 2;

        private const int MaritalStatusRow = 4;
        private const int MaritalStatusColumn = 1;

        public CSharpDeveloperQuestionnaireParser()
        {
        }

        public sealed override Task<ParsedData> Parse(string path)
        {
            using (var wordDoc = WordprocessingDocument.Open(path, false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;
                var candidateDataTable = body.ChildElements.Where(e => e.LocalName == "tbl").First();
                parsedData.Candidate = ParseCandidate(candidateDataTable);
                parsedData.Vacancy = VacancyParse(candidateDataTable);
                foreach (var cell in candidateDataTable.ExtractCellsFromTable())
                {
                    var questionsTable = cell.FirstOrDefault(c => c.LocalName == "tbl");
                    if (questionsTable != null)
                    {
                        parsedData.Questionnaire = ParseQuestionnaire(questionsTable);
                    }
                }
            }
            return Task.FromResult(parsedData);
        }

        private static Candidate ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ExtractRowsFromTable(false);
            Candidate candidate = new()
            {
                Name = rows.ExtractCellTextFromRow(FullNameRow, FullNameColumn),
                DateOfBirth = rows.TryExtractDate(DateOfBirthRow, DateOfBirthColumn),
                Address = rows.ExtractCellTextFromRow(AddressRow, AddressColumn).FindText(@"Адрес проживания[:]?.+", "Адрес проживания[:]?"),
                TelephoneNumber = rows.ExtractCellTextFromRow(TelephoneNumberRow, TelephoneNumberColumn),
                EmailAddress = rows.ExtractCellTextFromRow(EmailRow, EmailColumn).FindText(@"E-Mail.+", "E-Mail"),
                MaritalStatus = rows.ExtractCellTextFromRow(MaritalStatusRow, MaritalStatusColumn),
            };
            return candidate;
        }

        private static Vacancy VacancyParse(OpenXmlElement table)
        {
            var rows = table.ExtractRowsFromTable(false);
            var vacancyName = rows.ElementAt(0).InnerText;
            vacancyName = vacancyName[(vacancyName.IndexOf(':') + 1)..];
            vacancyName = vacancyName.Trim(' ');
            return new Vacancy() { Name = vacancyName };
        }

        private static QuestionnaireElement ParseQuestionnaire(OpenXmlElement table)
        {
            var questionnaire = QuestionnaireElement.CreateQuestionnaireElement(questionnaireName);
            foreach (var row in table.ExtractRowsFromTable())
            {
                if (questionnaire.AddChildElement(ParseQuestionCategory(row)) == false)
                {
                    questionnaire.CurrentChildElement.AddChildElement(ParseQuestion(row));
                }
            }
            return questionnaire;
        }

        private static QuestionnaireElement ParseQuestionCategory(OpenXmlElement row)
        {
            if (row.ChildElements.Count == 3)
            {
                var questionCategory = QuestionnaireElement.CreateQuestionnaireElement(
                    row.ChildElements.ElementAt(2).InnerText);
                return questionCategory;
            }
            return null;
        }

        private static QuestionnaireElement ParseQuestion(OpenXmlElement element)
        {
            if (element.ChildElements.Count == 5)
            {
                var question = QuestionnaireElement.CreateQuestionnaireElement(element.ChildElements[2].InnerText);
                question.AddChildElement(ParseAnswer(element));
                return question;
            }
            return null;
        }

        private static QuestionnaireElement ParseAnswer(OpenXmlElement child)
        {
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                Dictionary<string, string> properties = new()
                {
                    { "Name", child.ChildElements[4].InnerText },
                    { "Estimation", child.ChildElements[3].InnerText },
                };
                var answer = QuestionnaireElement.CreateQuestionnaireElement(properties);
                return answer;
            }
            return null;
        }
    }
}

