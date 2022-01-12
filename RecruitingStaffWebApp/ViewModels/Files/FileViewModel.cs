using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitingStaff.WebApp.ViewModels.Files
{
    public class FileViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileType { get; set; }

        public SelectList FileTypeSelectList { get; set; }

        public int? CandidateId { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? TestTaskId { get; set; }

    }
}
