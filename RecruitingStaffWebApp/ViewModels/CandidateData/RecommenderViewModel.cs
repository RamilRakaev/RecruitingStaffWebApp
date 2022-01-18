namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class RecommenderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CandidateId { get; set; }
        public int PreviousJobId { get; set; }
        public string PositionAtWork { get; set; }
        public string PhoneNumber { get; set; }
    }
}
