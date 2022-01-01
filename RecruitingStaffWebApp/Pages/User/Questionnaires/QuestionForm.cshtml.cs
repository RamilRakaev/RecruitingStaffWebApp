using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionFormModel : BasePageModel
    {
        public QuestionFormModel(IMediator mediator, ILogger<QuestionFormModel> logger) : base(mediator, logger)
        {
        }

        public int QuestionnaireId { get; set; }
        public QuestionViewModel Question { get; set; }
        public SelectList QuestionCategories { get; set; }

        public async Task OnGet(int? questionId, int? questionCategoryId, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
               QuestionCategories = new SelectList(
                await _mediator
                .Send(
                    new GetQuestionCategoriesByQuestionnaireIdQuery(questionnaireId)),
                "Id",
                "Name");

            if (questionId == null)
            {
                Question = new() { QuestionCategoryId = questionCategoryId ?? 0 };
            }
            else
            {
                Question = GetViewModel<Question, QuestionViewModel>(
                    await _mediator.Send(new GetEntityByIdQuery<Question>(questionId.Value)));
            }
        }

        public async Task<IActionResult> OnPost(QuestionViewModel question, int questionnaireId)
        {
            var questionEntity = GetEntity<Question, QuestionViewModel>(question);
            if (question.Id == 0)
            {
                await _mediator.Send(new CreateEntityCommand<Question>(questionEntity));
            }
            else
            {
                await _mediator.Send(new ChangeEntityCommand<Question>(questionEntity));
            }
            return RedirectToPage("ConcreteQuestionnaire", new { questionnaireId });
        }
    }
}
