using CQRS.Commands.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Contenders
{
    public class RemoveContenderHandler : IRequestHandler<RemoveContenderCommand, int>
    {
        private readonly IRepository<Contender> _contenderRepository;

        public RemoveContenderHandler(IRepository<Contender> contenderRepository)
        {
            _contenderRepository = contenderRepository;
        }

        public async Task<int> Handle(RemoveContenderCommand request, CancellationToken cancellationToken)
        {
            await _contenderRepository.RemoveAsync(request.Id);
            await _contenderRepository.SaveAsync();
            return request.Id;
        }
    }
}
