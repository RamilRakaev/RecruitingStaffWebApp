using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class GetFullCandidateDataHandler : IRequestHandler<GetFullCandidateDataQuery, Candidate>
    {
        private readonly IMediator _mediator;

        public GetFullCandidateDataHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Candidate> Handle(GetFullCandidateDataQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _mediator.Send(new GetEntityByIdQuery<Candidate>(request.CandidateId));
            var previousJobPlacements = await _mediator.Send(new GetEntitiesByForeignKeyQuery<PreviousJobPlacement>(e => e.CandidateId == request.CandidateId));
            foreach(var previousJobPlacement in previousJobPlacements)
            {
                var recommenders =
                    await _mediator.Send(
                        new GetEntitiesByForeignKeyQuery<Recommender>
                        (e => e.PreviousJobId == previousJobPlacement.Id));
                previousJobPlacement.Recommenders = recommenders.ToList();
            }
            var educations = await _mediator.Send(new GetEntitiesByForeignKeyQuery<Education>(e => e.CandidateId == request.CandidateId));
            var kids = await _mediator.Send(new GetEntitiesByForeignKeyQuery<Kid>(e => e.CandidateId == request.CandidateId));
            candidate.PreviousJobs = previousJobPlacements.ToList();
            candidate.Educations = educations.ToList();
            candidate.Kids = kids.ToList();
            return candidate;
        }
    }
}
