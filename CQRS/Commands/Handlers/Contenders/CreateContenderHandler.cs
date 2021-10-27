using CQRS.Commands.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Contenders
{
    public class CreateContenderHandler : IRequestHandler<CreateContenderCommand, Contender>
    {
        private readonly IRepository<Contender> _contenderRepository;

        public CreateContenderHandler(IRepository<Contender> contenderRepository)
        {
            _contenderRepository = contenderRepository;
        }

        public async Task<Contender> Handle(CreateContenderCommand request, CancellationToken cancellationToken)
        {
            await _contenderRepository.AddAsync(request.Contender);
            await _contenderRepository.SaveAsync();
            return request.Contender;
        }
    }
}
