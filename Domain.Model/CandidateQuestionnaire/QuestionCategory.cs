
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class QuestionCategory : CandidateQuestionnaireEntity
    {
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public List<Question> Questions { get; set; }
    }
}