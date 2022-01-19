using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Domain.Model.BaseEntities
{
    public class CandidateDataEntity : CandidatesSelectionEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
