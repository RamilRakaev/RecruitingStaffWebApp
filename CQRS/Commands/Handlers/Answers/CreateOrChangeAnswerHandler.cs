using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Answers
{
    public class CreateOrChangeAnswerHandler : AnswerCommandHandlers, IRequestHandler<CreateOrChangeAnswerCommand, bool>
    {
        public CreateOrChangeAnswerHandler(IRepository<Answer> answerRepository) : base(answerRepository)
        { }

        public async Task<bool> Handle(CreateOrChangeAnswerCommand request, CancellationToken cancellationToken)
        {
            await CreateOrChange(request.Answer, cancellationToken);
            return true;
        }
    }
}
