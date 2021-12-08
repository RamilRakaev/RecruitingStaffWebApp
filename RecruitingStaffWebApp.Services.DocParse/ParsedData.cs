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

        private QuestionnaireElement currentQuestionCategory { get; set; }
        private QuestionnaireElement currentQuestion { get; set; }
        private QuestionnaireElement currentAnswer { get; set; }

        public string FileExtension { get; set; }
        public string FileSource { get; set; }

        public Task AddQuestionnaire(string name)
        {
            Questionnaire = new()
            {
                Name = name,
                ChildElements = new(),
            };
            return Task.CompletedTask;
        }

        public Task AddQuestionCategory(string name)
        {
            currentQuestionCategory = new()
            {
                Name = name,
                ChildElements = new(),
            };
            Questionnaire.ChildElements.Add(currentQuestionCategory);
            return Task.CompletedTask;
        }

        public Task AddQuestion(string name)
        {
            currentQuestion = new()
            {
                Name = name,
                ChildElements = new(),
            };
            currentQuestionCategory.ChildElements.Add(currentQuestion);
            return Task.CompletedTask;
        }

        public Task AddAnswer(string text)
        {
            currentAnswer = new()
            {
                Name = text,
                ChildElements = new(),
            };
            currentQuestion.ChildElements.Add(currentAnswer);
            return Task.CompletedTask;
        }

        public Task AddAnswer(string text, string familiarWithTheTechnology)
        {
            currentAnswer = new()
            {
                Properties = new(),
            };
            currentAnswer.Properties.Add("FamiliarWithTheTechnology", familiarWithTheTechnology);
            currentAnswer.Properties.Add("Text", text);
            currentQuestion.ChildElements.Add(currentAnswer);
            return Task.CompletedTask;
        }
    }
}
