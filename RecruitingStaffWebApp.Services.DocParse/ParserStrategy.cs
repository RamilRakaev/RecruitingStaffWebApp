using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public abstract class ParserStrategy
    {
        protected QuestionCategory currentCategory;
        protected Question currentQuestion;

        protected readonly ParsedData parsedData;

        public ParserStrategy()
        {
            parsedData = new ParsedData();
        }

        public abstract Task<ParsedData> Parse(string path);
    }
}
