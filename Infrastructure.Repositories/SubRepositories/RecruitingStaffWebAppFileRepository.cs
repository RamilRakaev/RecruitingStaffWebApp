using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class RecruitingStaffWebAppFileRepository : BaseRepository<RecruitingStaffWebAppFile>
    {
        public RecruitingStaffWebAppFileRepository(DataContext context) : base(context)
        { }

        public override IQueryable<RecruitingStaffWebAppFile> GetAll()
        {
            return base.GetAll()
                .Include(f => f.Candidate)
                .Include(f => f.QuestionnaireId);
        }
    }
}
