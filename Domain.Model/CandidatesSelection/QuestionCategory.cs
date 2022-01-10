
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class QuestionCategory : CandidatesSelectionEntity 
    {
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public List<Question> Questions { get; set; }
    }
}