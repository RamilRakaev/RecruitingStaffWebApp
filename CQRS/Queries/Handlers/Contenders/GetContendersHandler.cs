using CQRS.Queries.Requests.Contenders;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Contenders.Handlers
{
    public class GetContendersHandler : IRequestHandler<GetContendersQuery, Contender[]>
    {
        private readonly IRepository<Contender> _contenderRepository;

        public GetContendersHandler(IRepository<Contender> contenderRepository)
        {
            _contenderRepository = contenderRepository;
        }

        public Task<Contender[]> Handle(GetContendersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_contenderRepository.GetAll().ToArray());
        }
    }
}
