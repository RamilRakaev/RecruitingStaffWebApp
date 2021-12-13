using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Model;
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

        public CSharpDeveloperQuestionnaireParser()
        {
        }

        public sealed override async Task<ParsedData> Parse(string path)
        {
            using (var wordDoc = WordprocessingDocument.Open(path, false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;
                var candidateDataTable = body.ChildElements.Where(e => e.LocalName == "tbl").First();
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
            return parsedData;
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ExtractRowsFromTable(false);
            var vacancyName = rows.ElementAt(0).InnerText;
            parsedData.Candidate = new()
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
            await parsedData.AddQuestionnaire(questionnaireName);

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
                parsedData.AddQuestionCategory(child.ChildElements.ElementAt(2).InnerText);
            }
            return Task.CompletedTask;
        }

        private async Task ParseQuestion(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 5)
            {
                await parsedData.AddQuestion(child.ChildElements[2].InnerText);
                await ParseAnswer(child);
            }
        }

        private Task ParseAnswer(OpenXmlElement child)
        {
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                QuestionnaireElement answer = new()
                {
                    Name = child.ChildElements[4].InnerText,
                };
                answer.Properties.Add("Estimation", child.ChildElements[3].InnerText);
                parsedData.AddAnswer(answer);
            }
            return Task.CompletedTask;
        }
    }
}

