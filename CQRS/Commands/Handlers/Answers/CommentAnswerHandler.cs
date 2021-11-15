using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class CommentAnswerHandler : IRequestHandler<CommentAnswerCommand, bool>
    {
        private readonly IRepository<Answer> _answeRepository;

        public CommentAnswerHandler(IRepository<Answer> answeRepository)
        {
            _answeRepository = answeRepository;
        }

        public async Task<bool> Handle(CommentAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answeRepository.FindAsync(request.AnswerId, cancellationToken);
            answer.Comment = request.Comment;
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            await _answeRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
