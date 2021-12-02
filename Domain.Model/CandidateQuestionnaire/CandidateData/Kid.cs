namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData
{
    public class Kid : BaseEntity
    {
        public string Gender { get; set; }
        public int Age { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
