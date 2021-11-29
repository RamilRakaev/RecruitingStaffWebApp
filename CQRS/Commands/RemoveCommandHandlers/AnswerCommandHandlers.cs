using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class AnswerCommandHandlers
    {
        protected readonly IRepository<Answer> _answerRepository;

        public AnswerCommandHandlers(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task CreateOrChange(Answer newAnswer, CancellationToken cancellationToken)
        {
            var oldAnswer = await _answerRepository.FindNoTrackingAsync(newAnswer.Id, cancellationToken);
            if (oldAnswer == null)
            {
                oldAnswer = await _answerRepository
                    .GetAllAsNoTracking()
                    .Where(a => a.Equals(newAnswer))
                    .FirstOrDefaultAsync(cancellationToken);
                if (oldAnswer == null)
                {
                    newAnswer.Question = null;
                    newAnswer.Candidate = null;
                    await CreateAnswer(newAnswer, cancellationToken);
                    return;
                }
            }
            await ChangeAnswer(newAnswer, cancellationToken);
        }

        public async Task CreateAnswer(Answer answer, CancellationToken cancellationToken)
        {
            await _answerRepository.AddAsync(answer, cancellationToken);
            await _answerRepository.SaveAsync(cancellationToken);
        }

        public async Task ChangeAnswer(Answer newAnswer, CancellationToken cancellationToken)
        {
            await _answerRepository.Update(newAnswer);
            await _answerRepository.SaveAsync(cancellationToken);
        }

        public async Task RemoveAnswer(int answerId, CancellationToken cancellationToken)
        {
            await _answerRepository.RemoveAsync(await _answerRepository.FindNoTrackingAsync(answerId, cancellationToken));
            await _answerRepository.SaveAsync(cancellationToken);
        }
    }
}
