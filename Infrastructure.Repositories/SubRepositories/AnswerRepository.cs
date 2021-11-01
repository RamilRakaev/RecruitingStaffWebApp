using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class AnswerRepository : BaseRepository<Answer>
    {
        public AnswerRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Answer> GetAll()
        {
            return _context.Set<Answer>().Include(a => a.Candidate).Include(a => a.Question);
        }

    }
}
