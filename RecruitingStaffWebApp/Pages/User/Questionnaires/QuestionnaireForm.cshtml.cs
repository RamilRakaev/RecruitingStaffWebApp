using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnaireFormModel : BasePageModel
    {
        public QuestionnaireFormModel(IMediator mediator) : base(mediator)
        {
        }

        public Questionnaire Questionnaire { get; set; }
        public SelectList Vacancies { get; set; }

        public async Task<IActionResult> OnGet(int? questionnaireId)
        {
            Vacancies = new SelectList(await _mediator.Send(new GetVacanciesQuery()), "Id", "Name");
            if (questionnaireId == null)
            {
                Questionnaire = new Questionnaire();
            }
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Questionnaire questionnaire)
        {
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire));
            return RedirectToPage("Questionnaires");
        }
    }
}