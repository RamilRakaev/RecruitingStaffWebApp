using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.CandidateQuestionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.CandidateQuestionnares
{
    public class GetAvailableQuestionnairesHandler : IRequestHandler<GetAvailableQuestionnairesQuery, Questionnaire[]>
    {
        private readonly IMediator _mediator;

        public GetAvailableQuestionnairesHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Questionnaire[]> Handle(GetAvailableQuestionnairesQuery request, CancellationToken cancellationToken)
        {
            var candidateVacancies =
                await _mediator.Send(
                    new GetEntitiesByForeignKeyQuery<CandidateVacancy>(cv => cv.FirstEntityId == request.CandidateId),
                    cancellationToken);
            var vacancyIds = candidateVacancies.Select(cv => cv.SecondEntityId);
            var questionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Questionnaire>(q => vacancyIds.Contains(q.VacancyId)),
                cancellationToken);
            return questionnaires;
        }
    }
}
