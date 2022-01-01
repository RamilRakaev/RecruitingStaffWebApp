using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.Maps
{
    public class CandidateTestTask : BaseMap
    {
        public Candidate Candidate { get; set; }
        public TestTask TestTask { get; set; }
    }
}
