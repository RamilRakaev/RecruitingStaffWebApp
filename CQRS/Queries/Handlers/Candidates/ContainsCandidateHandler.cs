using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class ContainsCandidateHandler : IRequestHandler<ContainsCandidateQuery, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public ContainsCandidateHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(ContainsCandidateQuery request, CancellationToken cancellationToken)
        {
            if(request.CandidateId == 0)
            {
                return await _candidateRepository
                    .GetAllAsNoTracking()
                    .Where(c => c.Name == request.CandidateName)
                    .FirstOrDefaultAsync(cancellationToken) != null;
            }
            return await _candidateRepository.FindNoTrackingAsync(request.CandidateId, cancellationToken) != null;
        }
    }
}
