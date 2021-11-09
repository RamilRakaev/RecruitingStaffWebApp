using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class VacancyRepository : BaseRepository<Vacancy>
    {
        public VacancyRepository(DataContext context) : base(context)
        { }
    }
}
