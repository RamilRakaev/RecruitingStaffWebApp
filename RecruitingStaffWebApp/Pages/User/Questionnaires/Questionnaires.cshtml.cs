using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnairesModel : BasePageModel
    {
        public QuestionnairesModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public Questionnaire CurrentQuestionnaire { get; set; }
        public QuestionnaireViewModel[] Questionnaires { get; set; }

        public async Task OnGet()
        {
            Questionnaires = GetViewModels<Questionnaire, QuestionnaireViewModel>(
                await _mediator.Send(new GetQuestionnairesQuery())
                );
        }

        public async Task OnPostRemoveQuestionnaire(int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionnaireCommand(questionnaireId));
            Questionnaires = GetViewModels<Questionnaire, QuestionnaireViewModel>(
                await _mediator.Send(new GetQuestionnairesQuery())
                );
        }

        public async Task OnPostRemoveQuestionCategory(int questionCategoryId)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            Questionnaires = GetViewModels<Questionnaire, QuestionnaireViewModel>(
                await _mediator.Send(new GetQuestionnairesQuery())
                );
        }
    }
}
