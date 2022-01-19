using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Kid : CandidateDataEntity
    {
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
