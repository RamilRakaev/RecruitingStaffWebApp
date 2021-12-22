
namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData
{
    public class Option : CandidateQuestionnaireEntity
    {
        public string Value { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}