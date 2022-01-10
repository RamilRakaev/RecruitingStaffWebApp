using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questions
{
    public class RemoveQuestionHandler : IRequestHandler<RemoveQuestionCommand, bool>
    {
        private readonly IMediator _mediator;

        public RemoveQuestionHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
        {
            var answers = await _mediator.Send(new GetEntitiesByForeignKeyQuery<Answer>(a => a.QuestionId == request.QestionId),
                cancellationToken);
            foreach (var answer in answers)
            {
                await _mediator.Send(new RemoveEntityCommand<Answer>(answer.Id),
                    cancellationToken);
            }
            await _mediator.Send(new RemoveEntityCommand<Question>(request.QestionId),
                cancellationToken);
            return true;
        }
    }
}
