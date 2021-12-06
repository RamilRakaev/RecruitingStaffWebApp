using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> Parse(string document, JobQuestionnaire jobQuestionnaire, bool parseQuestions = true);
    }
}
