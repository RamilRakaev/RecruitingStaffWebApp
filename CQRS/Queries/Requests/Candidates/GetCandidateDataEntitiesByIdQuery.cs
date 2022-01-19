using MediatR;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class GetCandidateDataEntitiesByIdQuery<TEntity> : IRequest<TEntity[]>
        where TEntity : CandidateDataEntity, new()
    {
        public GetCandidateDataEntitiesByIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}
