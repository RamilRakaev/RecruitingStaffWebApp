namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData
{
    public class Recommender : BaseEntity
    {
        public string Name { get; set; }
        public string PositionAtWork { get; set; }
        public string PhoneNumber { get; set; }

        public int PreviousJobId { get; set; }
        public PreviousJobPlacement PreviousJob { get; set; }
    }
}
