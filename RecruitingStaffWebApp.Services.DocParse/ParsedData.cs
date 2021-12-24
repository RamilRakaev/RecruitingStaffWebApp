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

        public QuestionnaireElement Questionnaire { get; set; }
        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        private QuestionnaireElement currentQuestionCategory;
        private QuestionnaireElement currentQuestion;
        private QuestionnaireElement currentAnswer;

        public string FileExtension { get; set; }
        public string FileSource { get; set; }

        public Task AddQuestionnaire(string name)
        {
            Questionnaire = new()
            {
                Name = name,
            };
            return Task.CompletedTask;
        }

        public void AddQuestionCategory(string name)
        {
            currentQuestionCategory = new()
            {
                Name = name,
            };
            Questionnaire.AddChildElement(currentQuestionCategory);
        }

        public void AddQuestion(string name)
        {
            currentQuestion = new()
            {
                Name = name,
            };
            currentQuestionCategory.AddChildElement(currentQuestion);
        }

        public void AddAnswer(string name)
        {
            currentAnswer = new();
            currentAnswer.Properties.Add("Name", name);
            currentQuestion.AddChildElement(currentAnswer);
        }

        public void AddAnswer(QuestionnaireElement answer)
        {
            currentQuestion.AddChildElement(answer);
        }
    }
}
