using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnaireFormModel : BasePageModel
    {
        public QuestionnaireFormModel(IMediator mediator, ILogger<QuestionnaireFormModel> logger) : base(mediator, logger)
        {
        }

        public Questionnaire Questionnaire { get; set; }
        public SelectList Vacancies { get; set; }

        public async Task OnGet(int? questionnaireId)
        {
            Vacancies = new SelectList(await _mediator.Send(new GetVacanciesQuery()), "Id", "Name");
            if (questionnaireId == null)
            {
                Questionnaire = new Questionnaire();
            }
            else
            {
                Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId.Value));
            }
        }

        public async Task<IActionResult> OnPost(Questionnaire questionnaire)
        {
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire));
            return RedirectToPage("Questionnaires");
        }
    }
}
