using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetEntitiesHandler<TEntity> : IRequestHandler<GetEntitiesQuery<TEntity>, TEntity[]>
        where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntitiesHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity[]> Handle(GetEntitiesQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsNoTracking().ToArrayAsync(cancellationToken);
        }
    }
}
