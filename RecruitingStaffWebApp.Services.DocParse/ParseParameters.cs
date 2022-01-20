namespace RecruitingStaffWebApp.Services.DocParse
{
    public class ParseParameters
    {
        public ParseParameters(string path)
        {
            Path = path;
        }

        public ParseParameters(string path, JobQuestionnaireType jobQuestionnaire, int candidateId, int questionnaireId)
        {
            Path = path;
            JobQuestionnaire = jobQuestionnaire;
            CandidateId = candidateId;
            QuestionnaireId = questionnaireId;
        }

        public string Path { get; }
        public JobQuestionnaireType JobQuestionnaire { get; }
        public int CandidateId { get; }
        public int QuestionnaireId { get; }
    }
}
