namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class CandidateOptionsViewModel
    {
        public CandidateViewModel CandidateViewModel { get; set; }
        public OptionViewModel[] OptionViewModels { get; set; }
        public bool ControlPanelDisplaying { get; set; } = true;
    }
}
