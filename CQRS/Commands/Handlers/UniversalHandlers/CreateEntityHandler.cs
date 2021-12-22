using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateEntityHandler<TEntity> : IRequestHandler<CreateEntityCommand<TEntity>, TEntity> where TEntity : CandidateQuestionnaireEntity
    {
        private readonly IRepository<TEntity> _repository;

        public CreateEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Handle(CreateEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Entity, cancellationToken);
            await _repository.SaveAsync(cancellationToken);
            return request.Entity;
        }
    }
}
