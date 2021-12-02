using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public static class OpenXmlElementExtensions
    {
        public static IEnumerable<OpenXmlElement> ExtractRowsFromTable(this OpenXmlElement element, bool firstElementDeleting = true)
        {
            var rows = element.ChildElements.Where(e => e.LocalName == "tr");
            return firstElementDeleting ? rows.Skip(1) : rows;
        }

        public static IEnumerable<OpenXmlElement> ExtractParagraphsFromRow(this OpenXmlElement element, bool notNulls = false)
        {
            List<OpenXmlElement> paragraphs = new();
            var cells = element.ChildElements.Where(e => e.LocalName == "tc");
            foreach (var cell in cells)
            {
                var nullableParagraphs =
                    cell
                    .ChildElements
                    .Where(e => e.LocalName == "p");

                nullableParagraphs =
                    notNulls ?
                    nullableParagraphs.Where(p => p.InnerText != "") : nullableParagraphs;
                paragraphs.AddRange(nullableParagraphs);
            }
            return paragraphs;
        }

        public static IEnumerable<OpenXmlElement> ExtractCellsFromRow(this OpenXmlElement element)
        {
            return element.ChildElements.Where(e => e.LocalName == "tc");
        }

        public static string ExtractCellTextFromRow(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex)
        {
            return rows
                .ElementAt(rowIndex)
                .ChildElements
                .Where(e => e.LocalName == "tc")
                .ElementAt(cellIndex).InnerText;
        }

        public static string ExtractCellTextFromRow(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex, string pattern)
        {
            Regex regex = new(pattern);
            var matches = regex.Matches(rows.ExtractCellTextFromRow(rowIndex, cellIndex));
            if (matches.Count > 0)
            {
                return matches[0].Value;
            }
            return "";
        }

        public static IEnumerable<OpenXmlElement> ExtractCellsFromTable(this OpenXmlElement element)
        {
            var rows = element.ChildElements.Where(e => e.LocalName == "tr").Reverse();
            List<OpenXmlElement> cells = new();
            foreach (var row in rows)
            {
                cells.AddRange(row.Where(r => r.LocalName == "tc"));
            }
            return cells;
        }

        public static DateTime TryExtractDate(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex)
        {
            Regex regex = new(@"\d+(.\d+)?(.\d+)?");
            var matches = regex.Matches(rows.ExtractCellTextFromRow(rowIndex, cellIndex));
            if (matches.Count > 0)
            {
                _ = DateTime.TryParse(matches[0].Value, out var date);
                return date;
            }
            return new DateTime();
        }

        public static string GetTextAfterCharacter(this string text, char character, int number = 1)
        {
            int index = 0;
            int numberOfIterations = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == character)
                {
                    index = i;
                    if (++numberOfIterations == number)
                    {
                        break;
                    }
                }
            }
            return text[(index + 1)..].Trim(' ');
        }

        public static string GetTextBeforeCharacter(this string text, char character)
        {
            return text[..text.IndexOf(character)].Trim(' ');
        }

        public static string ExtractTextAfterCharacterFromRow(this IEnumerable<OpenXmlElement> rows, in int rowIndex, in int cellIndex, in char character)
        {
            return rows.ExtractCellTextFromRow(rowIndex, cellIndex).GetTextAfterCharacter(character);
        }
    }
}
