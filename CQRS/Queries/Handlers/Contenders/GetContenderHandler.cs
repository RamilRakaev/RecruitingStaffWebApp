using CQRS.Queries.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Handlers.Contenders
{
    public class GetContenderHandler : IRequestHandler<GetContenderQuery, Contender>
    {
        private readonly IRepository<Contender> _contenderRepository;

        public GetContenderHandler(IRepository<Contender> contenderRepository)
        {
            _contenderRepository = contenderRepository;
        }

        public async Task<Contender> Handle(GetContenderQuery request, CancellationToken cancellationToken)
        {
            return await _contenderRepository.FindAsync(request.ContenderId);
        }
    }
}
