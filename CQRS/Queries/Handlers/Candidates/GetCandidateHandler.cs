using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

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
            return await _CandidateRepository.FindNoTrackingAsync(request.CandidateId, cancellationToken);
        }
    }
}
