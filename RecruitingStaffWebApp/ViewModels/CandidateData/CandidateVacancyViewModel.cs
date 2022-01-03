using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class CandidateVacancyViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string VacancyName { get; set; }
        public SelectList CandidateStatusSelectList { get; set; }
        public int FirstEntityId { get; set; }
        public int SecondEntityId { get; set; }
    }
}
