using Domain.Model;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class GetCandidatesQuery : IRequest<Candidate[]>
    {
    }
}
