using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.PreviousJobs;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.PreviousJobs
{
    public class GetRecommendersHandler : IRequestHandler<GetRecommendersQuery, Recommender[]>
    {
        private readonly IMediator _mediator;

        public GetRecommendersHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Recommender[]> Handle(GetRecommendersQuery request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetEntitiesByForeignKeyQuery<Recommender>(
                r => r.PreviousJobId == request.PreviousJobPlacementId),
                cancellationToken);
        }
    }
}
