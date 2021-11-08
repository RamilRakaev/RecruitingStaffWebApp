using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class CandidateQuestionnaireRepository : BaseRepository<CandidateQuestionnaire>
    {
        public CandidateQuestionnaireRepository(DataContext context) : base(context)
        { }

        public override IQueryable<CandidateQuestionnaire> GetAll()
        {
            return base.GetAll()
                .Include(cq => cq.Candidate)
                .Include(cq => cq.Questionnaire);
        }
    }
}
