using CQRS.Queries.Requests.Candidates;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Candidates.Handlers
{
    public class GetCandidatesHandler : IRequestHandler<GetCandidatesQuery, Candidate[]>
    {
        private readonly IRepository<Candidate> _CandidateRepository;

        public GetCandidatesHandler(IRepository<Candidate> CandidateRepository)
        {
            _CandidateRepository = CandidateRepository;
        }

        public Task<Candidate[]> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_CandidateRepository.GetAll().ToArray());
        }
    }
}
