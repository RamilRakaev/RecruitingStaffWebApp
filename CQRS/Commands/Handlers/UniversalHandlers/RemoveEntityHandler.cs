using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class RemoveEntityHandler<TEntity> : IRequestHandler<RemoveEntityCommand<TEntity>, TEntity>
        where TEntity : CandidateQuestionnaireEntity
    {

        private readonly IRepository<TEntity> _repository;

        public RemoveEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Handle(RemoveEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindNoTrackingAsync(request.EntityId, cancellationToken);
            await _repository.RemoveAsync(entity);
            await _repository.SaveAsync(cancellationToken);
            return entity;
        }
    }
}
