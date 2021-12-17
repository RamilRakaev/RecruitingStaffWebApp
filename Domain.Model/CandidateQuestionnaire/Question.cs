using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Question : BaseEntity
    {
        public List<Answer> Answers { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
    }
}
