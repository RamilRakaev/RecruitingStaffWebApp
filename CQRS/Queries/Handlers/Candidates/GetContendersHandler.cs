using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Interfaces;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Candidates.Handlers
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
