namespace RecruitingStaff.WebApp.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        public VacancyViewModel()
        {

        }

        public VacancyViewModel(object obj) : base(obj)
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }
    }
}
