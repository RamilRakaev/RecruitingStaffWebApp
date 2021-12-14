using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeEntityCommand<T> : IRequest<bool>
    {
        public CreateOrChangeEntityCommand(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; set; }
    }
}
