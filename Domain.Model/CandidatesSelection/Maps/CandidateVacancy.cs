using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.Maps
{
    public class CandidateVacancy : BaseMap
    {
        public CandidateStatus CandidateStatus { get; set; }
        public Candidate Candidate { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
