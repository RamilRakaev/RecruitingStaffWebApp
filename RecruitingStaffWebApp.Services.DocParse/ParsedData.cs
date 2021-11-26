using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public class ParsedData
    {
        public ParsedData()
        {
            Educations = new List<Education>();
            PreviousJobs = new List<PreviousJob>();
            Recommenders = new List<Recommender>();

            QuestionCategories = new List<QuestionCategory>();
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }

        //public ParsedData()
        //{
        //    Educations = new List<Education>();
        //    PreviousJobs = new List<PreviousJob>();
        //    Recommenders = new List<Recommender>();

        //    QuestionCategories = new List<QuestionCategory>();
        //    Questions = new List<Question>();
        //    Answers = new List<Answer>();

        //    Questionnaire = new Questionnaire();
        //    Questionnaire.QuestionCategories = QuestionCategories;
        //}

        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public List<Education> Educations { get; set; }
        public List<PreviousJob> PreviousJobs { get; set; }
        public List<Recommender> Recommenders { get; set; }

        public List<QuestionCategory> QuestionCategories { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }

        public string FileExtension { get; set; } = ".docx";
    }
}
