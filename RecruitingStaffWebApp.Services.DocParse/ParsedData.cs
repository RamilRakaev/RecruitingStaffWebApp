using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
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

        private QuestionCategory currentQuestionCategory;
        private Question currentQuestion;

        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public Dictionary<Question, Answer> AnswersOnQuestions { get; set; }
        public string FileExtension { get; set; }

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
