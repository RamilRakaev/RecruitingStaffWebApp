using System.Collections.Generic;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class Question : CandidatesSelectionEntity 
    {
        public List<Answer> Answers { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
    }
}
