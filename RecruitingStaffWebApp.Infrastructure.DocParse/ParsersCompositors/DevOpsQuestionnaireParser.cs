using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RecruitingStaffWebApp.Services.DocParse;
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

        public DevOpsQuestionnaireParser()
        {
            parsedData.Vacancy = new() { Name = "DevOps" };
        }

        private WorksheetPart wsPart;
        private WorkbookPart currentWbPart;
        public override async Task<ParsedData> Parse(string path)
        {
            await ParseQuestionnaire(path, "Лист1");
            return parsedData;
        }

        public async Task ParseQuestionnaire(string fileName, string sheetName)
        {
            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false))
                currentWbPart = document.WorkbookPart;

            Sheet theSheet = currentWbPart.Workbook.Descendants<Sheet>().
              Where(s => s.Name == sheetName).FirstOrDefault();

            if (theSheet == null)
            {
                throw new ArgumentException(null, nameof(sheetName));
            }

            wsPart =
                (WorksheetPart)(currentWbPart.GetPartById(theSheet.Id));
            List<Cell> cells = new();
            await parsedData.AddQuestionnaire(questionnaireName);
            int rowIndex = 5;
            await ParseHeader(ref rowIndex, startOfSequenceOfCapitalLetters + 1);
            while (await ParseQuestionCategory(ref rowIndex, startOfSequenceOfCapitalLetters + 1))
            { }

        }

        private Task ParseHeader(ref int rowIndex, int columnIndex)
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
            headers = cellsText.ToArray();
            return Task.CompletedTask;
        }

        private Task<bool> ParseQuestionCategory(ref int rowIndex, int letterOfAddressIndex)
        {
            var questionCategoryName = GetTextFromTheCell(
                ExtractCell((char)letterOfAddressIndex + rowIndex.ToString())
                );
            if (questionCategoryName == "")
            {
                return Task.FromResult(false);
            }
            parsedData.AddQuestionCategory(questionCategoryName);
            rowIndex++;
            while (true)
            {
                var secondCell = ExtractCell((char)(letterOfAddressIndex + 1) + rowIndex.ToString());
                if (GetTextFromTheCell(secondCell) == "")
                {
                    break;
                }
                var questionCell = ExtractCell((char)(letterOfAddressIndex) + rowIndex.ToString());
                parsedData.AddQuestion(GetTextFromTheCell(questionCell));
                ParseAnswers(rowIndex, letterOfAddressIndex + 1);
                rowIndex++;
            }
            return Task.FromResult(true);
        }

        private Task ParseAnswers(in int rowIndex, int letterOfAddressIndex)
        {
            for (int columnIndex = 1, letterIndex = letterOfAddressIndex; columnIndex < headers.Length; columnIndex++, letterIndex++)
            {
                string addressName = (char)letterIndex + rowIndex.ToString();
                var cell = ExtractCell(addressName);
                var cellText = GetTextFromTheCell(cell);
                if (cellText == "")
                {
                    break;
                }
                parsedData.AddAnswer($"{headers[columnIndex]}: {cellText}");
            }
            return Task.CompletedTask;
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
