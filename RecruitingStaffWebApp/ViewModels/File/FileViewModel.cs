namespace RecruitingStaff.WebApp.ViewModels.File
{
    public class FileViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FileViewModelType FileViewModelType { get; set; }

        public int? CandidateId { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? TestTaskId { get; set; }

    }
}
