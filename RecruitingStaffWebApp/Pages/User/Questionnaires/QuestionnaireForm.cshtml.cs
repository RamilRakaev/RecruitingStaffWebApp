using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
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
            Vacancies = new SelectList(await _mediator.Send(new GetVacanciesQuery()), "Id", "Name");
            if (questionnaireId == null)
            {
                Questionnaire = new QuestionnaireViewModel();
            }
            else
            {
                var questionnaireEntity = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId.Value));
                Questionnaire = GetViewModel<Questionnaire, QuestionnaireViewModel>(questionnaireEntity);
            }
        }

        public async Task<IActionResult> OnPost(QuestionnaireViewModel questionnaire)
        {
            var questionnaireEntity = GetEntity<Questionnaire, QuestionnaireViewModel>(questionnaire);
            await _mediator.Send(new CreateOrChangeEntityCommand<Questionnaire>(questionnaireEntity));
            return RedirectToPage("Questionnaires");
        }
    }
}
