using MediatR;
using RecruitingStaff.Domain.Model;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries
{
    public class GetEntitiesByForeignKeyQuery<TEntity> : IRequest<TEntity[]>
        where TEntity : BaseEntity
    {
        public GetEntitiesByForeignKeyQuery(Func<TEntity, bool> func)
        {
            Func = func;
        }

        public Func<TEntity, bool> Func { get; set; }
    }
}
