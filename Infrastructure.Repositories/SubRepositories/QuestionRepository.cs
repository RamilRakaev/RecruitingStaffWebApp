using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionRepository : BaseRepository<Question>
    {
        public QuestionRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Question> GetAll()
        {
            return _context
                .Set<Question>()
                .Include(q => q.Answers)
                .Include(q => q.QuestionCategory);
        }
    }
}
