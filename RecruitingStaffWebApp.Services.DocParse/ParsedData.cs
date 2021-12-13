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

        public Task AddQuestionCategory(string name)
        {
            currentQuestionCategory = new()
            {
                Name = name,
            };
            Questionnaire.ChildElements.Add(currentQuestionCategory);
            return Task.CompletedTask;
        }

        public Task AddQuestion(string name)
        {
            currentQuestion = new()
            {
                Name = name,
            };
            currentQuestionCategory.ChildElements.Add(currentQuestion);
            return Task.CompletedTask;
        }

        public Task AddAnswer(string text)
        {
            currentAnswer = new();
            currentAnswer.Properties.Add("Text", text);
            currentQuestion.ChildElements.Add(currentAnswer);
            return Task.CompletedTask;
        }

        public Task AddAnswer(QuestionnaireElement answer)
        {
            currentQuestion.ChildElements.Add(answer);
            return Task.CompletedTask;
        }
    }
}
