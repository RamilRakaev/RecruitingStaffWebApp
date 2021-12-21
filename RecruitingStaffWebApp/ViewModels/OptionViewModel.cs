namespace RecruitingStaff.WebApp.ViewModels
{
    public class OptionViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? CandidateId { get; set; }
    }
}
