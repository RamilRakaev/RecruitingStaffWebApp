using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> ParseQuestionnaire(string document, JobQuestionnaire jobQuestionnaire);

        public Task<bool> ParseAnswersAndCandidateData(string fileName, JobQuestionnaire jobQuestionnaire, int candidateId);
    }
}
