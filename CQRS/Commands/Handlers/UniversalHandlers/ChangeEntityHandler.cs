using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class ChangeEntityHandler<TEntity> : IRequestHandler<ChangeEntityCommand<TEntity>, TEntity>
        where TEntity : CandidateQuestionnaireEntity
    {
        private readonly IRepository<TEntity> _repository;

        public ChangeEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Handle(ChangeEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await _repository.Update(request.Entity);
            await _repository.SaveAsync(cancellationToken);
            return request.Entity;
        }
    }
}
