using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaffWebApp.Services.DocParse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public class ParsedData
    {
        public ParsedData(string fileExtension = ".docx")
        {
            FileExtension = fileExtension;
            AnswersOnQuestions = new();
        }

        public QuestionnaireElement QuestionnaireRp { get; set; }
        public VacancyParsedData VacancyParsedData { get; set; }
        public CandidateParsedData CandidateParsedData { get; set; }
        public int CandidateId { get; set; }

        private QuestionnaireElement currentQuestionCategoryRp { get; set; }
        private QuestionnaireElement currentQuestionRp { get; set; }
        private QuestionnaireElement currentAnswerRp { get; set; }

        public string FileExtension { get; set; }
        public string FileSource { get; set; }

        public Task AddQuestionnaire(string name)
        {
            QuestionnaireRp = new()
            {
                Name = name,
                ChildElements = new(),
            };
            return Task.CompletedTask;
        }

        public Task AddQuestionCategory(string name)
        {
            currentQuestionCategoryRp = new()
            {
                Name = name,
                Parent = QuestionnaireRp,
                ChildElements = new(),
            };
            QuestionnaireRp.ChildElements.Add(currentQuestionCategoryRp);
            return Task.CompletedTask;
        }

        public Task AddQuestion(string name)
        {
            currentQuestionRp = new()
            {
                Name = name,
                Parent = currentQuestionCategoryRp,
                ChildElements = new(),
            };
            currentQuestionCategoryRp.ChildElements.Add(currentQuestionRp);
            return Task.CompletedTask;
        }

        public Task AddAnswer(string name)
        {
            currentAnswerRp = new()
            {
                Name = name,
                Parent = currentQuestionRp,
                ChildElements = new(),
            };
            currentQuestionRp.ChildElements.Add(currentAnswerRp);
            return Task.CompletedTask;
        }

        private QuestionCategory currentQuestionCategory;
        private Question currentQuestion;

        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public Dictionary<Question, Answer> AnswersOnQuestions { get; set; }

        
        public Task AddQuestionnaire(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
            Questionnaire.QuestionCategories ??= new();
            return Task.CompletedTask;
        }

        public Task AddQuestionCategory(QuestionCategory questionCategory)
        {
            Questionnaire.QuestionCategories.Add(questionCategory);
            currentQuestionCategory = questionCategory;
            currentQuestionCategory.Questions ??= new();
            return Task.CompletedTask;
        }

        public Task AddQuestion(Question question)
        {
            currentQuestionCategory.Questions.Add(question);
            currentQuestion = question;
            currentQuestion.Answers ??= new();
            return Task.CompletedTask;
        }

        public Task AddAnswer(Answer answer)
        {
            AnswersOnQuestions.Add(currentQuestion, answer);
            return Task.CompletedTask;
        }
    }
}
