namespace RecruitingStaff.Domain.Model.Options
{
    public class WebAppOptions
    {
        public string CandidateDocumentsSource { get; set; }
        public string EmptyQuestionnairesSource { get; set; }
        public string TestTasksSource { get; set; }
        public string JpgPhotosSource { get; set; }
        public string PngPhotosSource { get; set; }
        public string DefaultUserEmail { get; set; }
        public string DefaultUserPassword { get; set; }
        private const string docxMime = "application/vnd.openxmlformants-officedocument.wordprocessingml.document";

        public string GetSource(FileType fileType)
        {
            return fileType switch
            {
                FileType.CompletedQuestionnaire => CandidateDocumentsSource,
                FileType.QuestionnaireExample => EmptyQuestionnairesSource,
                FileType.TestTask => TestTasksSource,
                FileType.JpgPhoto => JpgPhotosSource,
                FileType.PngPhoto => PngPhotosSource,
                _ => string.Empty,
            };
        }

        public static string GetMimeType(FileType fileType)
        {
            return fileType switch
            {
                FileType.CompletedQuestionnaire => docxMime,
                FileType.QuestionnaireExample => docxMime,
                FileType.TestTask => docxMime,
                FileType.JpgPhoto => "image/jpg",
                FileType.PngPhoto => "image/png",
                _ => string.Empty,
            };
        }
    }
}
