using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class CandidateQuestionnaire : BaseMap
    {
        public Questionnaire Questionnaire;
        public int QuestionnaireId;

        public Candidate Candidate;
        public int CandidateId;
    }
}
