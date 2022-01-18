using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.PreviousJobs
{
    public class GetRecommendersQuery : IRequest<Recommender[]>
    {
        public GetRecommendersQuery(int previousJobPlacementId)
        {
            PreviousJobPlacementId = previousJobPlacementId;
        }

        public int PreviousJobPlacementId { get; set; }
    }
}
