using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class RecruitingStaffWebAppFileRepository : BaseRepository<RecruitingStaffWebAppFile>
    {
        public RecruitingStaffWebAppFileRepository(DataContext context) : base(context)
        { }
    }
}
