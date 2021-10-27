using Domain.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<Entity> : IRepository<Entity> where Entity : BaseEntity
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Entity> FindAsync(int id)
        {
            return await _context.Set<Entity>().FindAsync(id);
        }

        public async Task<Entity> FindNoTrackingAsync(int id)
        {
            return await _context.Set<Entity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<Entity> GetAll()
        {
            return _context.Set<Entity>();
        }

        public IQueryable<Entity> GetAllAsNoTracking()
        {
            return _context.Set<Entity>().AsNoTracking();
        }

        public async Task AddAsync(Entity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task RemoveAsync(Entity entity)
        {
            _context.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
