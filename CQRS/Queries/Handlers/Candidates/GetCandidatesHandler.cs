using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Candidates.Handlers
{
    //public class GetCandidatesHandler : IRequestHandler<GetCandidatesQuery, Candidate[]>
    //{
    //    private readonly IRepository<Candidate> _candidateRepository;

    //    public GetCandidatesHandler(IRepository<Candidate> CandidateRepository)
    //    {
    //        _candidateRepository = CandidateRepository;
    //    }

    //    public Task<Candidate[]> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(_candidateRepository.GetAllAsNoTracking().ToArray());
    //    }
    //}
}
