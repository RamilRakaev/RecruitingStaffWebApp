using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
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
    }
}
