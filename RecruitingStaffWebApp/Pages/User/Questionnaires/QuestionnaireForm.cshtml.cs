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

        public QuestionnaireViewModel QuestionnaireViewModel { get; set; }
        public SelectList Vacancies { get; set; }

        public async Task OnGet(int? questionnaireId)
        {
            Vacancies = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            if (questionnaireId == null)
            {
                QuestionnaireViewModel = new QuestionnaireViewModel();
            }
            else
            {
                var questionnaireEntity = await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(questionnaireId.Value));
                QuestionnaireViewModel = GetViewModel<Questionnaire, QuestionnaireViewModel>(questionnaireEntity);
            }
        }

        public async Task<IActionResult> OnPost(QuestionnaireViewModel questionnaireViewModel)
        {
            if (ModelState.IsValid)
            {
                var questionnaireEntity = GetEntity<Questionnaire, QuestionnaireViewModel>(questionnaireViewModel);
                if (questionnaireViewModel.Id == 0)
                {
                    await _mediator.Send(new CreateEntityCommand<Questionnaire>(questionnaireEntity));
                }
                else
                {
                    await _mediator.Send(new ChangeEntityCommand<Questionnaire>(questionnaireEntity));
                }
                return RedirectToPage("Questionnaires");
            }
            ModelState.AddModelError("", "Неправильно введены данные");
            QuestionnaireViewModel = questionnaireViewModel;
            Vacancies = new SelectList(await _mediator.Send(new GetEntitiesQuery<Vacancy>()), "Id", "Name");
            return Page();
        }
    }
}
