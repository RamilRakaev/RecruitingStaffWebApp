using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class RemoveVacancyHandler : IRequestHandler<RemoveVacancyCommand, bool>
    {
        private readonly IMediator _mediator;

        public RemoveVacancyHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveVacancyCommand request, CancellationToken cancellationToken)
        {
            var questionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Questionnaire>(q => q.VacancyId == request.VacancyId));
            foreach (var questionnaire in questionnaires)
            {
                await _mediator.Send(new RemoveQuestionnaireCommand(questionnaire.Id));
            }
            await _mediator.Send(new RemoveEntityCommand<Vacancy>(request.VacancyId));
            return true;
        }
    }
}
