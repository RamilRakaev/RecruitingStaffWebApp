using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class Answer : CandidateQuestionnaireEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string FamiliarWithTheTechnology { get; set; }
        public byte Estimation { get; set; }
        public string Comment { get; set; }
    }
}