using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles
{
    public class GetSourceOfCandidatePhotoQuery : IRequest<string>
    {
        public GetSourceOfCandidatePhotoQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}
