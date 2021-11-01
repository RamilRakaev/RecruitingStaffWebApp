
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class QuestionCategory : BaseEntity
    {
        public string Name { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public List<Question> Questions { get; set; }
    }
}