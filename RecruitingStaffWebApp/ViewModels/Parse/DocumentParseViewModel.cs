using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.WebApp.ViewModels.Parse
{
    public class DocumentParseViewModel : BaseViewModel
    {
        public IFormFile FormFile { get; set; }
        public bool IsCompletedQuestionnaire { get; set; }

        public int CandidateId { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
