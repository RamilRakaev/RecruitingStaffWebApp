using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateOrChangeEntityByKeysHandler<TEntity> : IRequestHandler<CreateOrChangeEntityByKeysCommand<TEntity>, TEntity>
        where TEntity : CandidateQuestionnaireEntity, new()
    {
        private readonly IRepository<TEntity> _repository;
        private PropertyInfo[] _entityKeysProperties;
        private TEntity _currentEntity;

        public CreateOrChangeEntityByKeysHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Handle(CreateOrChangeEntityByKeysCommand<TEntity> request, CancellationToken cancellationToken)
        {
            _entityKeysProperties = typeof(TEntity)
                .GetProperties()
                .Where(p => p.Name.Contains("Id"))
                .ToArray();
            _currentEntity = request.Entity;

            var entity = await _repository.FindAsync(_currentEntity.Id, cancellationToken);
            if (entity == null)
            {
                entity = _repository
                    .GetAllAsNoTracking()
                    .Where(e => e.Name == _currentEntity.Name && FindingMatches(e))
                    .FirstOrDefault();
                await _repository.AddAsync(_currentEntity, cancellationToken);
            }
            else
            {
                await _repository.Update(_currentEntity);
            }
            await _repository.SaveAsync(cancellationToken);
            return request.Entity;
        }

        bool FindingMatches(TEntity entity)
        {
            foreach (var property in _entityKeysProperties)
            {
                if (property.GetValue(entity) != property.GetValue(_currentEntity))
                {
                    return false;
                }
            }
            return false;
        }
    }
}
