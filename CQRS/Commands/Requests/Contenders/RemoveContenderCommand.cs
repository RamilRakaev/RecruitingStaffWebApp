using Domain.Model;
using MediatR;

namespace CQRS.Commands.Requests.Contenders
{
    public class RemoveContenderCommand : IRequest<bool>
    {
        public RemoveContenderCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
