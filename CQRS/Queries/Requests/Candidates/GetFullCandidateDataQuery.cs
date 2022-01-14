using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class GetFullCandidateDataQuery : IRequest<Candidate>
    {
        public GetFullCandidateDataQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}
