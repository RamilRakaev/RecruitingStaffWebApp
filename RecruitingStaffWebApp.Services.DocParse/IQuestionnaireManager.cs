using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> ParseQuestionnaireExampleAsync(string document, JobQuestionnaire jobQuestionnaire);

        public Task<bool> ParseCompletedQuestionnaireAsync(string fileName, JobQuestionnaire jobQuestionnaire, int candidateId);
    }
}
