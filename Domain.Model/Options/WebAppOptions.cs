namespace RecruitingStaff.Domain.Model.Options
{
    public class WebAppOptions
    {
        public string CandidateDocumentsSource { get; set; }
        public string QuestionnairesSource { get; set; }
        public string TestTasksSource { get; set; }
        public string PhotosSource { get; set; }
        public string DefaultUserEmail { get; set; }
        public string DefaultUserPassword { get; set; }

        public string GetSource(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.CompletedQuestionnaire:
                    return CandidateDocumentsSource;
                case FileType.EmptyQuestionnaire:
                    return QuestionnairesSource;
                case FileType.TestTask:
                    return TestTasksSource;
                case FileType.Photo:
                    return PhotosSource;
                default: return string.Empty;
            }
        }
    }
}
