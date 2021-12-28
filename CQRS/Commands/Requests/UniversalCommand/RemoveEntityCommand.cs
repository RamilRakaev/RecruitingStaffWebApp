using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class RemoveEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : BaseEntity
    {
        public RemoveEntityCommand(int entityId)
        {
            EntityId = entityId;
        }

        public int EntityId { get; set; }
    }
}
