using MediatR;
using RecruitingStaff.Domain.Model;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : CandidatesSelectionEntity , new()
    {
        public CreateEntityCommand(TEntity entity)
        {
            if (entity.Id != 0) throw new ArgumentException();
            Entity = entity;
        }
        public TEntity Entity { get; set; }
    }
}
