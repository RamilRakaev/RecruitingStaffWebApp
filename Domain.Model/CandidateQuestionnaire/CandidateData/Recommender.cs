namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Recommender : CandidateQuestionnaireEntity
    {
        public string PositionAtWork { get; set; }
        public string PhoneNumber { get; set; }

        public int PreviousJobId { get; set; }
        public PreviousJobPlacement PreviousJob { get; set; }
    }
}
