using Domain.Model;
using MediatR;

namespace CQRS.Queries.Requests.Candidates
{
    public class GetCandidateQuery : IRequest<Candidate>
    {
        public GetCandidateQuery(int id)
        {
            CandidateId = id;
        }

        public int CandidateId { get; set; }
    }
}
