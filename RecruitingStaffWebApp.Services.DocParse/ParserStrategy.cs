using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public abstract class ParserStrategy
    {
        protected readonly WebAppOptions _options;

        protected QuestionCategory currentCategory;
        protected Question currentQuestion;

        protected readonly ParsedData parsedData;

        public ParserStrategy(WebAppOptions options)
        {
            _options = options;
            parsedData = new ParsedData();
        }

        public abstract Task<ParsedData> Parse(string fileName, bool parseAnswers = false);
    }
}
