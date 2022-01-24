using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> ParseAndSaveQuestionnaireExampleAsync(ParseParameters parseParameters);

        public Task<bool> ParseAndSaveCompletedQuestionnaireAsync(ParseParameters parseParameters);
    }
}
