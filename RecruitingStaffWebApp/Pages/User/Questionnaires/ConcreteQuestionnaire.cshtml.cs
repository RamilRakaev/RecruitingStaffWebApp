using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class ConcreteQuestionnaireModel : BasePageModel
    {
        public ConcreteQuestionnaireModel(IMediator mediator, ILogger<ConcreteQuestionnaireModel> logger) : base(mediator, logger)
        {
        }

        public Questionnaire Questionnaire { get; set; }
        public QuestionCategory[] QuestionCategories { get; set; }

        public async Task OnGet(int questionnaireId)
        {
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            QuestionCategories = await _mediator.Send(new GetQuestionCategoriesByQuestionnaireIdQuery(Questionnaire.Id));
        }

        public async Task OnPost(int questionnaireId, int questionCategoryId)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            QuestionCategories = await _mediator.Send(new GetQuestionCategoriesByQuestionnaireIdQuery(Questionnaire.Id));
        }
    }
}
