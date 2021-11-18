using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class ParsedData
    {
        public ParsedData()
        {
            QuestionCategories = new List<QuestionCategory>();
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }

        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }


        public List<QuestionCategory> QuestionCategories { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
