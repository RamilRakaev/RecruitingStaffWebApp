using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class RecruitingStaffWebAppFile : CandidateQuestionnaireEntity
    {
        public FileType FileType { get; set; }

        /// <summary>
        /// Photos, completed questionnaires
        /// </summary>
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int? QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public int? TestTaskId { get; set; }
        public TestTask TestTask { get; set; }
    }
}
