using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options
{
    public class GetOptionsByCandidateIdQuery : IRequest<Option[]>
    {
        public GetOptionsByCandidateIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }
        public int CandidateId { get; set; }
    }
}
