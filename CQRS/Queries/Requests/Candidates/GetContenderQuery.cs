using Domain.Model;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
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
