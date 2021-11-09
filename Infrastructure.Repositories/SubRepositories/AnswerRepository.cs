using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class AnswerRepository : BaseRepository<Answer>
    {
        public AnswerRepository(DataContext context) : base(context)
        { }
    }
}
