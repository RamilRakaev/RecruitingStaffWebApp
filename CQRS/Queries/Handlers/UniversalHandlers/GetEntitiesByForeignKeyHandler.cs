using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetEntitiesByForeignKeyHandler<TEntity> : IRequestHandler<GetEntitiesByForeignKeyQuery<TEntity>, TEntity[]>
    where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntitiesByForeignKeyHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public Task<TEntity[]> Handle(
            GetEntitiesByForeignKeyQuery<TEntity> request,
            CancellationToken cancellationToken)
        {
            var entities = _repository.GetAllAsNoTracking().Where(request.Func);
            if (entities != null)
            {
                return Task.FromResult(entities.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
