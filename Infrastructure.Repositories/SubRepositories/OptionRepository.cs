using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SubRepositories
{
    public class OptionRepository : BaseRepository<Option>
    {
        public OptionRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Option> GetAll()
        {
            return _context
                .Set<Option>()
                .Include(o => o.Candidate);
        }

        public override IQueryable<Option> GetAllAsNoTracking()
        {
            return _context
                .Set<Option>()
                .Include(o => o.Candidate)
                .AsNoTracking();
        }

        public override async Task<Option> FindAsync(int id)
        {
            return await _context
                .Set<Option>()
                .Include(o => o.Candidate)
                .FirstAsync(c => c.Id == id);
        }

        public override async Task<Option> FindNoTrackingAsync(int id)
        {
            return await _context
                .Set<Option>()
                .Include(o => o.Candidate)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
