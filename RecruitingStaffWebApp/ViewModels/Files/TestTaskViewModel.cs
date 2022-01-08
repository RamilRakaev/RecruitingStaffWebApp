using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitingStaff.WebApp.ViewModels.Files
{
    public class TestTaskViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int VacancyId { get; set; }
        public SelectList VacanciesSelectList { get; set; }
    }
}
