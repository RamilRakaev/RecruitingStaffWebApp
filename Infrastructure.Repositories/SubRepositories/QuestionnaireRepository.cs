using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Linq;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class QuestionnaireRepository : BaseRepository<Questionnaire>
    {
        public QuestionnaireRepository(DataContext context) : base(context)
        { }

        public override IQueryable<Questionnaire> GetAll()
        {
            var questionnaires = _context
                .Set<Questionnaire>()
                .Include(q => q.Vacancy)
                .Include(q => q.DocumentFiles)
                .Include("QuestionCategories.Questions")
                .Include("CandidateQuestionnaires.Candidate");
            return questionnaires;
        }
    }
}
