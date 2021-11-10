using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionCategoryRemoveHandler : QuestionCommandHandlers
    {
        protected readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public QuestionCategoryRemoveHandler(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository)
            : base(answerRepository, questionRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task RemoveQuestionCategory(int questionCategoryId)
        {
            var questionCategory = await _questionCategoryRepository.FindNoTrackingAsync(questionCategoryId);
            var questions = _questionRepository.GetAllAsNoTracking().Where(q => q.QuestionCategoryId == questionCategoryId);
            if(questions != null)
            {
                foreach (var question in questions.ToArray())
                {
                    await RemoveQuestion(question.Id);
                }
            }
            await _questionCategoryRepository.RemoveAsync(questionCategory);
            await _questionCategoryRepository.SaveAsync();
        }
    }
}
