using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public class ParsedData
    {
        public ParsedData(string fileExtension = ".docx")
        {
            FileExtension = fileExtension;
        }

        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public Dictionary<Question, Answer> AnswersOnQuestions { get; set; }
        public string FileExtension { get; set; }
    }
}
