using Domain.Model;
using MediatR;

namespace CQRS.Queries.Requests.Candidates
{
    public class GetCandidatesQuery : IRequest<Candidate[]>
    {
    }
}
