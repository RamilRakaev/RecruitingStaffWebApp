using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Interfaces;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
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
