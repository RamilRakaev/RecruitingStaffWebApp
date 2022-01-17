using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.PreviousJobs
{
    public class GetJobsByCandidateIdQuery : IRequest<PreviousJobPlacement[]>
    {
        public GetJobsByCandidateIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}
