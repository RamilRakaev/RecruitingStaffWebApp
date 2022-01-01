
namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Option : CandidateQuestionnaireEntity
    {
        public string Value { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}