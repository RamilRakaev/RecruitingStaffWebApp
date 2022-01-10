using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class RemoveQuestionCategoryHandler : IRequestHandler<RemoveQuestionCategoryCommand, bool>
    {
        private readonly IMediator _mediator;

        public RemoveQuestionCategoryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questions = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Question>(q => q.QuestionCategoryId == request.QestionCategoryId),
                cancellationToken);
            foreach(var question in questions)
            {
                await _mediator.Send(new RemoveQuestionCommand(question.Id), cancellationToken);
            }
            await _mediator.Send(new RemoveEntityCommand<QuestionCategory>(request.QestionCategoryId), cancellationToken);
            return true;
        }
    }
}
