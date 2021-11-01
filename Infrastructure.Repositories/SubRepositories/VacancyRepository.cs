using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class VacancyRepository : BaseRepository<Vacancy>
    {
        public VacancyRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Vacancy> GetAll()
        {
            return _context
                .Set<Vacancy>()
                .Include(o => o.Candidates);
        }

        public override IQueryable<Vacancy> GetAllAsNoTracking()
        {
            return _context
                .Set<Vacancy>()
                .Include(o => o.Candidates)
                .AsNoTracking();
        }

        public override async Task<Vacancy> FindAsync(int id)
        {
            return await _context
                .Set<Vacancy>()
                .Include(o => o.Candidates)
                .FirstAsync(c => c.Id == id);
        }

        public override async Task<Vacancy> FindNoTrackingAsync(int id)
        {
            return await _context
                .Set<Vacancy>()
                .Include(o => o.Candidates)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
