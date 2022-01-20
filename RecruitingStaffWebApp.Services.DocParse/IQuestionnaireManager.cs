using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface IQuestionnaireManager
    {
        public Task<bool> ParseQuestionnaireExampleAsync(ParseParameters parseParameters);

        public Task<bool> ParseCompletedQuestionnaireAsync(ParseParameters parseParameters);
    }
}
