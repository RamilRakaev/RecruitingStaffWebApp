using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class GetQuestionnairesHandler : IRequestHandler<GetQuestionnairesCommand, Questionnaire[]>
    {
        private readonly IRepository<Questionnaire> _questionnaiRerepository;

        public GetQuestionnairesHandler(IRepository<Questionnaire> questionnaiRerepository)
        {
            _questionnaiRerepository = questionnaiRerepository;
        }

        public Task<Questionnaire[]> Handle(GetQuestionnairesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_questionnaiRerepository.GetAllAsNoTracking().ToArray());
        }
    }
}
