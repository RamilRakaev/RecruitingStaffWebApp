
namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Answer : BaseEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        //public string Text { get; set; }
        public byte Estimation { get; set; }
        public string Comment { get; set; }
    }
}