using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Candidate> GetAll()
        {
            return base.GetAll()
                .Include(c => c.Questionnaires)
                .Include(c => c.CandidateVacancies)
                .Include(c => c.Options)
                .Include(c => c.Answers)
                .Include(c => c.Photo);
        }
    }
}
