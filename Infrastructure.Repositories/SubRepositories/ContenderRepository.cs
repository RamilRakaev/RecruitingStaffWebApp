using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SubRepositories
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Candidate> GetAll()
        {
            return _context
                .Set<Candidate>()
                .Include(c => c.Options);
        }

        public override IQueryable<Candidate> GetAllAsNoTracking()
        {
            return _context
                .Set<Candidate>()
                .AsNoTracking();
        }

        public override async Task<Candidate> FindAsync(int id)
        {
            return await _context
                .Set<Candidate>()
                .Include(c => c.Options)
                .FirstAsync(c => c.Id == id);
        }

        public override async Task<Candidate> FindNoTrackingAsync(int id)
        {
            return await _context
                .Set<Candidate>()
                .Include(c => c.Options)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
