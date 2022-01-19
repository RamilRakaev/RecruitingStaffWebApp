using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Option : CandidatesSelectionEntity 
    {
        public string Value { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}