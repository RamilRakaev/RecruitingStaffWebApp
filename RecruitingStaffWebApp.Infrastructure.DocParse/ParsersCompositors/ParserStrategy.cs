using DocumentFormat.OpenXml;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public abstract class ParserStrategy
    {
        private protected readonly WebAppOptions _options;

        private protected string questionnaireName;
        private protected QuestionCategory currentCategory;
        private protected Question currentQuestion;

        private protected readonly ParsedData parsedData;

        public ParserStrategy(IOptions<WebAppOptions> options)
        {
            _options = options.Value;
            parsedData = new ParsedData();
        }

        public abstract Task<ParsedData> Parse(string fileName);

        private protected static string ExtractCellTextFromRow(IEnumerable<OpenXmlElement> rows, int rowInd, int cellInd)
        {
            return rows
                .ElementAt(rowInd)
                .ChildElements
                .Where(e => e.LocalName == "tc")
                .ElementAt(cellInd).InnerText;
        }
    }
}
