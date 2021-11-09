using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionCategoryRepository : BaseRepository<QuestionCategory>
    {
        public QuestionCategoryRepository(DataContext context) : base(context)
        {
        }
    }
}
