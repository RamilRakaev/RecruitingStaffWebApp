using MediatR;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries
{
    public class GetEntitiesQuery<TEntity> : IRequest<TEntity[]>
        where TEntity : BaseEntity
    {
    }
}
