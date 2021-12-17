namespace RecruitingStaff.WebApp.ViewModels.Questionnaire
{
    public class QuestionnaireViewModel : BaseViewModel
    {
        public QuestionnaireViewModel()
        {

        }

        public QuestionnaireViewModel(object obj) : base(obj)
        {

        }

        public int Id { get; set; }
        public int VacancyId { get; set; }
        public string Name { get; set; }
    }
}
