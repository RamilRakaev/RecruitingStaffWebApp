using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
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
        public Question Question { get; set; }
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
                Question = await _mediator.Send(new GetQuestionByIdQuery(questionId.Value));
            }
        }

        public async Task<IActionResult> OnPost(Question question, int questionnaireId)
        {
            await _mediator.Send(new CreateOrChangeQuestionCommand(question));
            return RedirectToPage("ConcreteQuestionnaire", new { questionnaireId });
        }
    }
}
