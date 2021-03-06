using RecruitingStaff.WebApp.ViewModels.CandidateData;

namespace RecruitingStaff.WebApp.ViewModels.Questionnaire
{
    public class AnswerViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string FamiliarWithTheTechnology { get; set; }
        public byte Estimation { get; set; }
        public string Comment { get; set; }
        public string QuestionName { get; set; }
        public CandidateViewModel[] CandidateViewModels { get; set; }
        public string NameFragmentOfCandidate { get; set; }
    }
}
