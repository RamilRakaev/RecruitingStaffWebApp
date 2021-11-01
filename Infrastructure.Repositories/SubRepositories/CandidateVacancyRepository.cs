using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class CandidateVacancyRepository : BaseRepository<CandidateVacancy>
    {
        public CandidateVacancyRepository(DataContext context) : base(context)
        { }

        public override IQueryable<CandidateVacancy> GetAll()
        {
            return base.GetAll()
                .Include(c => c.Candidate)
                .Include(c => c.Vacancy);
        }
    }
}
