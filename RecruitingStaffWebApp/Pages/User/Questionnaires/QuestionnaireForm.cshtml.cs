using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnaireFormModel : BasePageModel
    {
        public QuestionnaireFormModel(IMediator mediator, ILogger<QuestionnaireFormModel> logger) : base(mediator, logger)
        {
        }

        public QuestionnaireViewModel Questionnaire { get; set; }
        public SelectList Vacancies { get; set; }

        public async Task OnGet(int? questionnaireId)
        {
            Vacancies = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            if (questionnaireId == null)
            {
                Questionnaire = new QuestionnaireViewModel();
            }
            else
            {
                var questionnaireEntity = await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(questionnaireId.Value));
                Questionnaire = GetViewModel<Questionnaire, QuestionnaireViewModel>(questionnaireEntity);
            }
        }

        public async Task<IActionResult> OnPost(QuestionnaireViewModel questionnaire)
        {
            var questionnaireEntity = GetEntity<Questionnaire, QuestionnaireViewModel>(questionnaire);
            if(questionnaire.Id == 0)
            {
                await _mediator.Send(new CreateEntityCommand<Questionnaire>(questionnaireEntity));
            }
            else
            {
                await _mediator.Send(new ChangeEntityCommand<Questionnaire>(questionnaireEntity));
            }
            return RedirectToPage("Questionnaires");
        }
    }
}
