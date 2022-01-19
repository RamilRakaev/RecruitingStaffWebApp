using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.BaseEntities;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetEntityByIdHandler<TEntity> : IRequestHandler<GetEntityByIdQuery<TEntity>, TEntity>
        where TEntity : CandidatesSelectionEntity , new()
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntityByIdHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public Task<TEntity> Handle(GetEntityByIdQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return _repository.FindNoTrackingAsync(request.EntityId, cancellationToken);
        }
    }
}
