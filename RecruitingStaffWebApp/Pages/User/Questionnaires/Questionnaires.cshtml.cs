using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnairesModel : BasePageModel
    {
        public QuestionnairesModel(IMediator mediator, ILogger<QuestionnairesModel> logger) : base(mediator, logger)
        {
        }

        public Questionnaire[] Questionnaires { get; set; }

        public async Task OnGet()
        {
            Questionnaires = await _mediator.Send(new GetQuestionnairesQuery());
        }

        public async Task OnPost(int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionnaireCommand(questionnaireId));
            Questionnaires = await _mediator.Send(new GetQuestionnairesQuery());
        }
    }
}
