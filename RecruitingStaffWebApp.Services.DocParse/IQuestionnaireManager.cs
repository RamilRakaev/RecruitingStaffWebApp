using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> ParseAndSaveQuestions(string document, JobQuestionnaire jobQuestionnaire);

        public Task<bool> ParseAndSaveAnswers(string document, JobQuestionnaire jobQuestionnaire);
    }
}
