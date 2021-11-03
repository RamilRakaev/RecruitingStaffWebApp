using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public interface ISaveParseQuestionnare
    {
        public Task Parse(string document);
    }
}
