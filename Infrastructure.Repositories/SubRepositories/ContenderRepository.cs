using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SubRepositories
{
    public class ContenderRepository : BaseRepository<Contender>
    {
        public ContenderRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Contender> GetAll()
        {
            return _context
                .Set<Contender>()
                .Include(c => c.Options);
        }

        public override IQueryable<Contender> GetAllAsNoTracking()
        {
            return _context
                .Set<Contender>()
                .AsNoTracking();
        }

        public override async Task<Contender> FindAsync(int id)
        {
            return await _context
                .Set<Contender>()
                .Include(c => c.Options)
                .FirstAsync(c => c.Id == id);
        }

        public override async Task<Contender> FindNoTrackingAsync(int id)
        {
            return await _context
                .Set<Contender>()
                .Include(c => c.Options)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
