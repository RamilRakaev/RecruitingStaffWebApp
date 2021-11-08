using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionCommandHandlers : AnswerCommandHandlers
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionCommandHandlers(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository) : base(answerRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task RemoveQuestion(int questionId)
        {
            var question = await _questionRepository.FindNoTrackingAsync(questionId);
            await RemoveAnswer(question.Id);
            await _questionRepository.RemoveAsync(question);
            await _questionRepository.SaveAsync();
        }
    }
}
