using RecruitingStaffWebApp.Services.DocParse.Model;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public class ParsedData
    {
        public ParsedData(string fileExtension = ".docx")
        {
            FileExtension = fileExtension;
        }

        public bool IsCompletedQuestionnaire { get; set; }
        public QuestionnaireElement Questionnaire { get; set; }
        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
        public int QuestionnaireId { get; set; }

        public string FileExtension { get; set; }
        public string FileSource { get; set; }

    }
}
