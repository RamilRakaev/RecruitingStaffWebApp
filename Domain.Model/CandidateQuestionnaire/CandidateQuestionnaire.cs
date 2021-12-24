using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class CandidateQuestionnaire : BaseMap
    {
        public CandidateQuestionnaire()
        {

        }

        public Candidate Candidate;
        public Questionnaire Questionnaire;
    }
}
