using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.PreviousJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.PreviousJobs
{
    public class GetJobsByCandidateIdHandler : IRequestHandler<GetJobsByCandidateIdQuery, PreviousJobPlacement[]>
    {
        private IRepository<PreviousJobPlacement> _previousJobPlacementRepository;

        public GetJobsByCandidateIdHandler(IRepository<PreviousJobPlacement> previousJobPlacementRepository)
        {
            _previousJobPlacementRepository = previousJobPlacementRepository;
        }

        public async Task<PreviousJobPlacement[]> Handle(GetJobsByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            return await _previousJobPlacementRepository
                .GetAllExistingEntitiesAsNoTracking()
                .Where(pjp => pjp.CandidateId == request.CandidateId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
