using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionRepository : BaseRepository<Question>
    {
        public QuestionRepository(DataContext context) : base(context)
        { }
    }
}
