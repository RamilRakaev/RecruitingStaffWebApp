using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionnaireRepository : BaseRepository<Questionnaire>
    {
        public QuestionnaireRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Questionnaire> GetAll()
        {
            return _context
                .Set<Questionnaire>()
                .Include(q => q.Vacancy)
                .Include(q => q.DocumentFile)
                .Include(q => q.QuestionCategories);
        }
    }
}
