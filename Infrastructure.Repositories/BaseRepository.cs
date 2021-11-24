using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.Repositories
{
    public class BaseRepository<Entity> : IRepository<Entity> where Entity : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public virtual IQueryable<Entity> GetAll()
        {
            return _context.Set<Entity>();
        }

        public virtual IQueryable<Entity> GetAllAsNoTracking()
        {
            return GetAll().AsNoTracking();
        }

        public virtual async Task<Entity> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task<Entity> FindNoTrackingAsync(int id, CancellationToken cancellationToken)
        {
            return await GetAll().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task Update(Entity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task AddAsync(Entity entity, CancellationToken cancellationToken)
        {
            await _context.Set<Entity>().AddAsync(entity, cancellationToken);
        }

        public virtual Task RemoveAsync(Entity entity)
        {
            return Task.FromResult(_context.Remove(entity));
        }

        public virtual async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
