using MediatR;
using RecruitingStaff.Domain.Model;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class ChangeEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : BaseEntity
    {
        public ChangeEntityCommand(TEntity entity)
        {
            if (entity.Id == 0) throw new ArgumentException("Нулевой идентификатор");
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
