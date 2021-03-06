using MediatR;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeEntityByKeysCommand<TEntity> : IRequest<TEntity> where TEntity : CandidatesSelectionEntity , new()
    {
        public CreateOrChangeEntityByKeysCommand(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
