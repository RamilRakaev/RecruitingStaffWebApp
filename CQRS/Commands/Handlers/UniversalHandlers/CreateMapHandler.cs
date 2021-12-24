using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateMapHandler<TMap> : IRequestHandler<CreateMapCommand<TMap>, TMap>
        where TMap : BaseMap, new()
    {
        private readonly IRepository<TMap> _repository;

        public CreateMapHandler(IRepository<TMap> repository)
        {
            _repository = repository;
        }

        public async Task<TMap> Handle(CreateMapCommand<TMap> request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Map, cancellationToken);
            await _repository.SaveAsync(cancellationToken);
            return request.Map;
        }
    }
}
