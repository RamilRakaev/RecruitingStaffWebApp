using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class CandidateVacancyRepository : BaseRepository<CandidateVacancy>
    {
        public CandidateVacancyRepository(DataContext context) : base(context)
        { }
    }
}
