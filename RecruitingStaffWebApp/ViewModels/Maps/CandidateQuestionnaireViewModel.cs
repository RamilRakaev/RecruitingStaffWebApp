using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitingStaff.WebApp.ViewModels.Maps
{
    public class CandidateQuestionnaireViewModel
    {
        public int Id { get; set; }
        public string QuestionnaireName { get; set; }
        public int FirstEntityId { get; set; }
        public int SecondEntityId { get; set; }
    }
}
