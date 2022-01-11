using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class DevOpsQuestionnaireParser : ParserStrategy
    {
        private const string questionnaireName = "Анкета девопса";
        private const int startOfSequenceOfCapitalLetters = 65;
        private const int endOfSequenceOfCapitalLetters = 91;
        private string[] headers;

        private WorksheetPart wsPart;
        private WorkbookPart currentWbPart;

        public DevOpsQuestionnaireParser()
        {
            parsedData.Vacancy = new() { Name = "DevOps" };
        }

        public override Task<ParsedData> ParseAsync(string path)
        {
            parsedData.Questionnaire = ParseQuestionnaire(path, "Лист1");
            return  Task.FromResult(parsedData);
        }

        public QuestionnaireElement ParseQuestionnaire(string fileName, string sheetName)
        {
            using SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false);
                currentWbPart = document.WorkbookPart;
            var f1 = currentWbPart.Workbook;
            Sheet theSheet = currentWbPart.Workbook.Descendants<Sheet>().
              Where(s => s.Name == sheetName).FirstOrDefault();

            if (theSheet == null)
            {
                throw new ArgumentException(null, nameof(sheetName));
            }

            wsPart =
                (WorksheetPart)(currentWbPart.GetPartById(theSheet.Id));
            List<Cell> cells = new();
            parsedData.Questionnaire = QuestionnaireElement.CreateQuestionnaireElement(questionnaireName);
            int rowIndex = 5;
            headers = ParseHeader(ref rowIndex, startOfSequenceOfCapitalLetters + 1);
            var questionnaire = QuestionnaireElement.CreateQuestionnaireElement(questionnaireName);
            while (questionnaire.AddChildElement(ParseQuestionCategory(ref rowIndex, startOfSequenceOfCapitalLetters + 1)))
            { }
            return questionnaire;
        }

        private string[] ParseHeader(ref int rowIndex, int columnIndex)
        {
            List<string> cellsText = new();
            while (true)
            {
                char letter = (char)columnIndex;
                var cell = ExtractCell(letter + rowIndex.ToString());
                var cellText = GetTextFromTheCell(cell);
                if (cellText == "")
                {
                    break;
                }
                cellsText.Add(cellText);
                columnIndex++;
            }
            rowIndex++;
            //headers = cellsText.ToArray();
            return cellsText.ToArray();
        }

        private QuestionnaireElement ParseQuestionCategory(ref int rowIndex, int letterOfAddressIndex)
        {
            var questionCategoryName = GetTextFromTheCell(
                ExtractCell((char)letterOfAddressIndex + rowIndex.ToString())
                );
            if (questionCategoryName == "")
            {
                return null;
            }
            var questionCategory = QuestionnaireElement.CreateQuestionnaireElement(questionCategoryName);
            rowIndex++;
            while (true)
            {
                var secondCell = ExtractCell((char)(letterOfAddressIndex + 1) + rowIndex.ToString());
                if (GetTextFromTheCell(secondCell) == "")
                {
                    break;
                }
                var questionCell = ExtractCell((char)(letterOfAddressIndex) + rowIndex.ToString());
                var question = questionCategory.CreateChildElement(GetTextFromTheCell(questionCell));
                question.AddRangeElements(ParseAnswers(rowIndex, letterOfAddressIndex + 1));
                rowIndex++;
            }
            return questionCategory;
        }

        private IEnumerable<QuestionnaireElement> ParseAnswers(in int rowIndex, int letterOfAddressIndex)
        {
            List<QuestionnaireElement> answers = new();
            for (int columnIndex = 1, letterIndex = letterOfAddressIndex; columnIndex < headers.Length; columnIndex++, letterIndex++)
            {
                string addressName = (char)letterIndex + rowIndex.ToString();
                var cell = ExtractCell(addressName);
                var cellText = GetTextFromTheCell(cell);
                if (cellText == "")
                {
                    break;
                }
                answers.Add(QuestionnaireElement.CreateQuestionnaireElement($"{headers[columnIndex]}: {cellText}"));
            }
            return answers;
        }

        private Cell ExtractCell(string addressName)
        {
            return wsPart
                        .Worksheet
                        .Descendants<Cell>()
                        .Where(c => c.CellReference.Value == addressName)
                        .FirstOrDefault();
        }

        private string GetTextFromTheCell(Cell theCell)
        {
            if (theCell != null)
            {
                var value = theCell.InnerText;

                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            var stringTable =
                                currentWbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            value = value switch
                            {
                                "0" => "FALSE",
                                _ => "TRUE",
                            };
                            break;
                    }
                }
                return value;
            }
            return string.Empty;
        }
    }
}
