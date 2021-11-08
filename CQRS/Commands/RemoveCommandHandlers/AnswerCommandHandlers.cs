using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class AnswerCommandHandlers
    {
        private readonly IRepository<Answer> _answerRepository;

        public AnswerCommandHandlers(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task CreateAnswer(Answer answer)
        {
            await _answerRepository.AddAsync(answer);
            await _answerRepository.SaveAsync();
        }

        public async Task ChangeAnswer(Answer newAnswer)
        {
            var oldAnswer = await _answerRepository.FindAsync(newAnswer.Id);
            oldAnswer.Comment = newAnswer.Comment;
            oldAnswer.Estimation = newAnswer.Estimation;
            await _answerRepository.SaveAsync();
        }

        public async Task RemoveAnswer(int answerId)
        {
            await _answerRepository.RemoveAsync(await _answerRepository.FindNoTrackingAsync(answerId));
            await _answerRepository.SaveAsync();
        }
    }
}
