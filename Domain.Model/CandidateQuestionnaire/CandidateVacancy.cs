using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class CandidateVacancy : BaseMap
    {
        public CandidateVacancy()
        {

        }

        public Candidate Candidate { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
