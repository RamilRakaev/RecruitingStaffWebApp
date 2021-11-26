using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public static class OpenXmlElementExtensions
    {
        public static IEnumerable<OpenXmlElement> ExtractRowsFromTable(this OpenXmlElement element)
        {
            return element.ChildElements.Where(e => e.LocalName == "tr").Skip(1);
        }

        public static IEnumerable<OpenXmlElement> ExtractCellsFromRow(this OpenXmlElement element)
        {
            return element.ChildElements.Where(e => e.LocalName == "tc");
        }

        public static IEnumerable<OpenXmlElement> ExtractRowsFromCandidateDataTable(this OpenXmlElement element)
        {
            return element.ChildElements.Where(e => e.LocalName == "tr");
        }

        public static string ExtractCellTextFromRow(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex)
        {
            return rows
                .ElementAt(rowIndex)
                .ChildElements
                .Where(e => e.LocalName == "tc")
                .ElementAt(cellIndex).InnerText;
        }

        public static IEnumerable<OpenXmlElement> ExtractCellsFromTable(this OpenXmlElement element)
        {
            var rows = element.ChildElements.Where(e => e.LocalName == "tr").Reverse();
            List<OpenXmlElement> cells = new List<OpenXmlElement>();
            foreach (var row in rows)
            {
                cells.AddRange(row.Where(r => r.LocalName == "tc"));
            }
            return cells;
        }

        public static DateTime TryExtractDate(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex)
        {
            _ = DateTime.TryParse(rows.ExtractCellTextFromRow(rowIndex, cellIndex), out var date);
            return date;
        }

        public static string GetTextAfterCharacter(this string text, char character)
        {
            return text[(text.IndexOf(character) + 1)..].Trim(' ');
        }

        public static string ExtractTextAfterCharacterFromRow(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex, in char character)
        {
            return rows.ExtractCellTextFromRow(rowIndex, cellIndex).GetTextAfterCharacter(character);
        }
    }
}
