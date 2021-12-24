using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public class CreateOrChangeEntityHandler<TEntity> : IRequestHandler<CreateOrChangeEntityCommand<TEntity>, TEntity>
        where TEntity : CandidateQuestionnaireEntity, new()
    {
        private readonly DataContext _context;

        public CreateOrChangeEntityHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Handle(CreateOrChangeEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var _repository = new BaseRepository<TEntity>(_context);
            var entity = await _repository
                .FindNoTrackingAsync(request.Entity.Id, cancellationToken)
                ?? await _repository
                .GetAllAsNoTracking()
                .Where(e => e.Name == request.Entity.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                await _repository.AddAsync(request.Entity, cancellationToken);
            }
            else
            {
                request.Entity.Id = entity.Id;
                await _repository.Update(request.Entity);
            }
            await _repository.SaveAsync(cancellationToken);
            return request.Entity;
        }
    }
}
