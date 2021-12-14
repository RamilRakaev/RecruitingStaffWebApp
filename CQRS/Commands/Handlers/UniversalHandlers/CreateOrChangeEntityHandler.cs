using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.UniversalHandlers
{
    public interface ICreateOrChangeEntityHandler<T> : IRequestHandler<CreateOrChangeEntityCommand<T>, bool> where T : BaseEntity
    {

    }
    public class CreateOrChangeEntityHandler<T> : ICreateOrChangeEntityHandler<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public CreateOrChangeEntityHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateOrChangeEntityCommand<T> request, CancellationToken cancellationToken)
        {
            var entity = await _repository
                .FindAsync(request.Entity.Id, cancellationToken)
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
            return true;
        }
    }
}
