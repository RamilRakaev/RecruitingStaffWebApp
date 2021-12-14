using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class RecruitingStaffWebAppFile : BaseEntity
    {
        //public string Name { get; set; }
        public FileType FileType { get; set; }

        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int? QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
