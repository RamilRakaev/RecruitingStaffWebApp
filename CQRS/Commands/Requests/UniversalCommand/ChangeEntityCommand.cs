using MediatR;
using RecruitingStaff.Domain.Model;
using System;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class ChangeEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : CandidateQuestionnaireEntity
    {
        public ChangeEntityCommand(TEntity entity)
        {
            if (entity.Id == 0) throw new ArgumentException();
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
