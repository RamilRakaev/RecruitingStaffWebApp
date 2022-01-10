using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.UniversalHandlers
{
    public class GetEntityByNameHandler<TEntity> : IRequestHandler<GetEntityByNameQuery<TEntity>, TEntity>
        where TEntity : CandidatesSelectionEntity 
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntityByNameHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public Task<TEntity> Handle(GetEntityByNameQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _repository
                .GetAllAsNoTracking()
                .Where(e => e.Name == request.Name)
                .FirstOrDefault());
        }
    }
}
