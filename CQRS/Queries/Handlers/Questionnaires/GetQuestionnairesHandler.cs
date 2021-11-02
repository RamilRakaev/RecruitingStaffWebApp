using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questionnaires
{
    public class GetQuestionnairesHandler : IRequestHandler<GetQuestionnairesQuery, Questionnaire[]>
    {
        private readonly IRepository<Questionnaire> _questionnaiRerepository;

        public GetQuestionnairesHandler(IRepository<Questionnaire> questionnaiRerepository)
        {
            _questionnaiRerepository = questionnaiRerepository;
        }

        public Task<Questionnaire[]> Handle(GetQuestionnairesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_questionnaiRerepository.GetAllAsNoTracking().ToArray());
        }
    }
}
