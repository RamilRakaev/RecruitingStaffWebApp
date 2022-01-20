using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.BaseEntities;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class GetCandidateDataEntitiesByIdHandler<TEntity> : IRequestHandler<GetCandidateDataEntitiesByIdQuery<TEntity>, TEntity[]>
        where TEntity : CandidateDataEntity, new()
    {
        private readonly IRepository<TEntity> _repository;

        public GetCandidateDataEntitiesByIdHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity[]> Handle(GetCandidateDataEntitiesByIdQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return await _repository
                .GetAllExistingEntitiesAsNoTracking()
                .Where(e => e.CandidateId == request.CandidateId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
