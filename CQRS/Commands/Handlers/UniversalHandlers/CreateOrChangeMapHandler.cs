using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateOrChangeMapHandler<TMap> : IRequestHandler<CreateOrChangeMapCommand<TMap>, TMap>
    where TMap : BaseMap, new()
    {
        private readonly IRepository<TMap> _repository;

        public CreateOrChangeMapHandler(IRepository<TMap> repository)
        {
            _repository = repository;
        }

        public async Task<TMap> Handle(CreateOrChangeMapCommand<TMap> request, CancellationToken cancellationToken)
        {
            var map = await _repository
                .GetAllAsNoTracking()
                .Where(m => m.FirstEntityId == request.Map.FirstEntityId
            && m.SecondEntityId == request.Map.SecondEntityId)
                .FirstOrDefaultAsync(cancellationToken);
            if (map == null)
            {
                await _repository.AddAsync(request.Map, cancellationToken);
            }
            else
            {
                request.Map.Id = map.Id;
                await _repository.Update(request.Map);
            }
            await _repository.SaveAsync(cancellationToken);
            return request.Map;
        }
    }
}
