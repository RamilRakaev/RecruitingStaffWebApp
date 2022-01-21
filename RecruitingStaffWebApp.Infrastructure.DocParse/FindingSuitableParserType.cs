using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class FindingSuitableParserType
    {
        private const string docxType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private const string xlsxType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private static readonly string[] csSharpKeywords = { ".net", "c#" };
        private static readonly string[] phpKeywords = { "php", "laravel" };

        public static ParserStrategy FindParserByFile(string path, string contentType)
        {
            if (contentType == xlsxType)
            {
                return new DevOpsQuestionnaireParser();
            }
            else if (contentType == docxType)
            {
                using var wordDoc = WordprocessingDocument.Open(path, false);
                var body = wordDoc.MainDocumentPart.Document.Body;
                var tables = body.ChildElements.Where(e => e.LocalName == "tbl");
                if (tables.Count() > 1)
                {
                    return new OfficeQuestionnaireParser();
                }
                else
                {
                    return PhpAndCSharpComparison(tables);
                }
            }
            throw new ParserNotFoundException();
        }

        private static ParserStrategy PhpAndCSharpComparison(IEnumerable<OpenXmlElement> tables)
        {
            string vacancyName = tables.First().ExtractRowsFromTable(false).ElementAt(0).InnerText.ToLower();
            foreach (string key in csSharpKeywords)
            {
                if (vacancyName.Contains(key))
                {
                    return new CSharpDeveloperQuestionnaireParser();
                }
            }
            foreach (string key in phpKeywords)
            {
                if (vacancyName.Contains(key))
                {
                    return new PhpDeveloperQuestionnaireParser();
                }
            }
            throw new ParserNotFoundException();
        }

        public static ParserStrategy ParserSubstitution(JobQuestionnaireType jobQuestionnaire)
        {
            return jobQuestionnaire switch
            {
                JobQuestionnaireType.CSharpDeveloperQuestionnaire =>
                     new CSharpDeveloperQuestionnaireParser(),
                JobQuestionnaireType.PhpDeveloperQuestionnaire =>
                    new PhpDeveloperQuestionnaireParser(),
                JobQuestionnaireType.OfficeQuestionnaire =>
                    new OfficeQuestionnaireParser(),
                JobQuestionnaireType.DevOpsQuestionnaire =>
                    new DevOpsQuestionnaireParser(),
                _ => throw new ParseException()
            };
        }
    }
}
