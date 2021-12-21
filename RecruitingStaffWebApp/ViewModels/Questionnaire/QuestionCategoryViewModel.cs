namespace RecruitingStaff.WebApp.ViewModels.Questionnaire
{
    public class QuestionCategoryViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionnaireId { get; set; }
        public QuestionViewModel[] Questions { get; set; }
    }
}
