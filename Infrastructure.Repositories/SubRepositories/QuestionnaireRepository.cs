using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionnaireRepository : BaseRepository<Questionnaire>
    {
        public QuestionnaireRepository(DataContext context) : base(context)
        { }
    }
}
