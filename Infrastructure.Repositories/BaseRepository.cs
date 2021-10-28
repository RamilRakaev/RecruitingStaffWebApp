using Domain.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
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
            return _context.Set<Entity>().AsNoTracking();
        }

        public virtual async Task<Entity> FindAsync(int id)
        {
            return await _context.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<Entity> FindNoTrackingAsync(int id)
        {
            return await _context.Set<Entity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task AddAsync(Entity entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual Task RemoveAsync(Entity entity)
        {
            return Task.FromResult(_context.Remove(entity));
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
