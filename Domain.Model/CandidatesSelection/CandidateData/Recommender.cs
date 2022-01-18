namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Recommender : CandidateDataEntity
    {
        public string PositionAtWork { get; set; }
        public string PhoneNumber { get; set; }

        public int PreviousJobId { get; set; }
        public PreviousJobPlacement PreviousJob { get; set; }
    }
}
