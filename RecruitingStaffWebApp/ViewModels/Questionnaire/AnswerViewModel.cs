namespace RecruitingStaff.WebApp.ViewModels.Questionnaire
{
    public class AnswerViewModel : BaseViewModel
    {
        public AnswerViewModel()
        {

        }

        public AnswerViewModel(object obj) : base(obj)
        {

        }

        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string FamiliarWithTheTechnology { get; set; }
        public byte Estimation { get; set; }
        public string Comment { get; set; }
    }
}
