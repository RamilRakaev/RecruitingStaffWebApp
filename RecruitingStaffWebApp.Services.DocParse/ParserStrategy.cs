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

        public abstract Task<ParsedData> Parse(string fileName);

        //protected Task LinkTheQuestionnaireAndTheCandidate()
        //{
        //    parsedData.CandidateQuestionnaire = new()
        //    {
        //        CandidateId = parsedData.Candidate.Id,
        //        QuestionnaireId = parsedData.Questionnaire.Id,
        //    };
        //    return Task.CompletedTask;
        //}

        //protected Task LinkTheVacancyAndTheCandidate()
        //{
        //    parsedData.CandidateVacancy = new()
        //    {
        //        CandidateId = parsedData.Candidate.Id,
        //        VacancyId = parsedData.Vacancy.Id,
        //    };
        //    return Task.CompletedTask;
        //}
    }
}
