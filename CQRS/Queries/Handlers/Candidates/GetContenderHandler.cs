using CQRS.Queries.Requests.Candidates;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Handlers.Candidates
{
    public class GetCandidateHandler : IRequestHandler<GetCandidateQuery, Candidate>
    {
        private readonly IRepository<Candidate> _CandidateRepository;

        public GetCandidateHandler(IRepository<Candidate> CandidateRepository)
        {
            _CandidateRepository = CandidateRepository;
        }

        public async Task<Candidate> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            return await _CandidateRepository.FindAsync(request.CandidateId);
        }
    }
}
