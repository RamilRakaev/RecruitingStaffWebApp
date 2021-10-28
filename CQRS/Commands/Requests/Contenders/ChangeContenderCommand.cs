using Domain.Model;
using MediatR;

namespace CQRS.Commands.Requests.Contenders
{
    public class ChangeContenderCommand : IRequest<bool>
    {
        public ChangeContenderCommand(Contender contender)
        {
            Contender = contender;
        }

        public Contender Contender { get; set; }
    }
}
