using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionCategoryRemoveHandler : QuestionCommandHandlers
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public QuestionCategoryRemoveHandler(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository)
            : base(answerRepository, questionRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task RemoveQuestionCategory(int QuestionCategoryId)
        {
            var questionCategory = await _questionCategoryRepository.FindNoTrackingAsync(QuestionCategoryId);
            foreach(var question in questionCategory.Questions)
            {
                await RemoveQuestion(question.Id);
            }
            await _questionCategoryRepository.RemoveAsync(questionCategory);
            await _questionCategoryRepository.SaveAsync();
        }
    }
}
