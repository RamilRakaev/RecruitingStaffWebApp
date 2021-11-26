using DocumentFormat.OpenXml.Packaging;
using RecruitingStaff.Domain.Model;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class OfficeQuestionnaireParser : ParserStrategy
    {
        public OfficeQuestionnaireParser(WebAppOptions options) : base(options)
        {
        }

        public override Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var table = body.ChildElements.Where(e => e.LocalName == "tbl").First();

                //await ParseCandidate(table);
                //await CreateQuestionnaire();
                var questionCategories = table.Last().Where(e => e.LocalName == "tc").First();
                //await ParseQuestionCategory(questionCategories);
            }
            return null;
        }
    }
}
