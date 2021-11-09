using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class CandidateQuestionnaireRepository : BaseRepository<CandidateQuestionnaire>
    {
        public CandidateQuestionnaireRepository(DataContext context) : base(context)
        { }
    }
}
