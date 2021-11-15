using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionCommandHandlers : AnswerCommandHandlers
    {
        protected readonly IRepository<Question> _questionRepository;

        public QuestionCommandHandlers(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository) : base(answerRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task RemoveQuestion(int questionId, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindNoTrackingAsync(questionId, cancellationToken);
            var answersIds = _answerRepository.GetAllAsNoTracking().Where(a => a.QuestionId == questionId).Select(a => a.Id).ToArray();
            foreach (var answerId in answersIds)
            {
                await RemoveAnswer(answerId, cancellationToken);
            }
            await _questionRepository.RemoveAsync(question);
            await _questionRepository.SaveAsync(cancellationToken);
        }
    }
}
