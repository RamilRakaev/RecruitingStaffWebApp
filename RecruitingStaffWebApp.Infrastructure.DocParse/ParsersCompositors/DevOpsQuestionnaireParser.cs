using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RecruitingStaff.Domain.Model;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class DevOpsQuestionnaireParser : ParserStrategy
    {
        private const int startOfSequenceOfCapitalLetters = 65;
        private const int endOfSequenceOfCapitalLetters = 91;


        public DevOpsQuestionnaireParser(WebAppOptions options) : base(options)
        {
        }

        private WorksheetPart wsPart;
        private WorkbookPart currentWbPart;
        public override async Task<ParsedData> Parse(string fileName)
        {
            await ParseQuestionnaire($"{_options.DocumentsSource}\\{fileName}", "Лист1");
            return parsedData;
        }

        public async Task<string> ParseQuestionnaire(string fileName, string sheetName)
        {
            string value = null;

            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false))
            {
                currentWbPart = document.WorkbookPart;

                Sheet theSheet = currentWbPart.Workbook.Descendants<Sheet>().
                  Where(s => s.Name == sheetName).FirstOrDefault();

                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                wsPart =
                    (WorksheetPart)(currentWbPart.GetPartById(theSheet.Id));
                List<Cell> cells = new();
                await parsedData.AddQuestionnaire("Анкета девопса");
                int rowIndex = 6;
                while (await ParseQuestionCategory(ref rowIndex, startOfSequenceOfCapitalLetters + 1))
                {

                }
                for (int i = 7; i < 28; i++)
                {
                    for (int j = startOfSequenceOfCapitalLetters + 1; j < 71; j++)
                    {
                        char letter = (char)j;
                        var cell = wsPart
                            .Worksheet
                            .Descendants<Cell>()
                            .Where(c => c.CellReference.Value == letter + i
                            .ToString());
                    }
                }
            }

            return value;
        }

        private Task<bool> ParseQuestionCategory(ref int rowIndex, int columnIndex)
        {
            var questionCategoryName = GetTextFromTheCell(
                ExtractCell((char)columnIndex + rowIndex.ToString())
                );
            if (questionCategoryName == "")
            {
                return Task.FromResult(false);
            }
            parsedData.AddQuestionCategory(questionCategoryName);
            rowIndex++;
            while (true)
            {
                var secondCell = ExtractCell((char)(columnIndex + 1) + rowIndex.ToString());
                if (GetTextFromTheCell(secondCell) == "")
                {
                    break;
                }
                var questionCell = ExtractCell((char)(columnIndex) + rowIndex.ToString());
                parsedData.AddQuestion(GetTextFromTheCell(questionCell));
                for (int j = columnIndex + 1; ; j++)
                {
                    char letter = (char)j;
                    var cell = ExtractCell(letter + rowIndex.ToString());
                    var cellText = GetTextFromTheCell(cell);
                    if (cellText == "")
                    {
                        break;
                    }
                    parsedData.AddAnswer(cellText);
                }

                rowIndex++;
            }
            return Task.FromResult(true);
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
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
                return value;
            }
            return string.Empty;
        }
    }
}
