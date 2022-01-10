namespace RecruitingStaff.Domain.Model.Options
{
    public class WebAppOptions
    {
        public string CandidateDocumentsSource { get; set; }
        public string EmptyQuestionnairesSource { get; set; }
        public string TestTasksSource { get; set; }
        public string DefaultUserEmail { get; set; }
        public string DefaultUserPassword { get; set; }

        public string GetSource(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.CompletedQuestionnaire:
                    return CandidateDocumentsSource;
                case FileType.QuestionnaireExample:
                    return EmptyQuestionnairesSource;
                case FileType.TestTask:
                    return TestTasksSource;
                default: return string.Empty;
            }
        }
    }
}
