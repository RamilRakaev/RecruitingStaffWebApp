using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

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
                .Include(o => o.CandidateVacancies)
                .Include(o => o.Questionnaires);
        }
    }
}
