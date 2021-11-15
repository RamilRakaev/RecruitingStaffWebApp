using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class RemoveAnswerHandler : AnswerCommandHandlers, IRequestHandler<RemoveAnswerCommand, bool>
    {
        public RemoveAnswerHandler(IRepository<Answer> answerRepository) : base(answerRepository)
        { }

        public async Task<bool> Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
        {
            await RemoveAnswer(request.AnswerId, cancellationToken);
            return true;
        }
    }
}
