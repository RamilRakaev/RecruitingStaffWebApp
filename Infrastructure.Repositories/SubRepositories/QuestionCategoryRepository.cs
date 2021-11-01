using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionCategoryRepository : BaseRepository<QuestionCategory>
    {
        public QuestionCategoryRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<QuestionCategory> GetAll()
        {
            return base.GetAll()
                .Include(q => q.Questionnaire)
                .Include(q => q.Questions);
        }
    }
}
