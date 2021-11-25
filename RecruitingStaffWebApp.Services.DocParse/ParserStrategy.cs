using DocumentFormat.OpenXml;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public abstract class ParserStrategy
    {
        protected readonly WebAppOptions _options;

        protected string questionnaireName;
        protected QuestionCategory currentCategory;
        protected Question currentQuestion;

        protected readonly ParsedData parsedData;

        public ParserStrategy(WebAppOptions options)
        {
            _options = options;
            parsedData = new ParsedData();
        }

        public abstract Task<ParsedData> Parse(string fileName);

        protected static string ExtractCellTextFromRow(IEnumerable<OpenXmlElement> rows, int rowInd, int cellInd)
        {
            return rows
                .ElementAt(rowInd)
                .ChildElements
                .Where(e => e.LocalName == "tc")
                .ElementAt(cellInd).InnerText;
        }
    }
}
