using Domain.Model;
using MediatR;

namespace CQRS.Commands.Requests.Contenders
{
    public class CreateContenderCommand : IRequest<Contender>
    {
        public CreateContenderCommand(Contender contender)
        {
            Contender = contender;
        }

        public Contender Contender { get; set; }
    }
}
