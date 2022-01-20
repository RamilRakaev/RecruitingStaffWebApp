using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class GetCandidatesByNameFragmentHandler : IRequestHandler<GetCandidatesByNameFragmentQuery, Candidate[]>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public GetCandidatesByNameFragmentHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public Task<Candidate[]> Handle(GetCandidatesByNameFragmentQuery request, CancellationToken cancellationToken)
        {
            var candidates = _candidateRepository
                .GetAllExistingEntitiesAsNoTracking()
                .Where(c => c.Name.Contains(request.NameFragment));
            if (candidates != null)
                return Task.FromResult(candidates.ToArray());
            else
                return Task.FromResult(Array.Empty<Candidate>());
        }
    }
}
