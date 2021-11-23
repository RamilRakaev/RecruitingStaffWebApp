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

        public async Task OnGet(int? questionId, int? questionCategoryId, int quesionnaireId)
        {
            QuestionCategories = new SelectList(
                await _mediator.Send(new GetQuestionCategoriesByQuestionnaireIdQuery(quesionnaireId)), "Id", "Name");
            QuestionnaireId = quesionnaireId;
            if (questionId == null)
            {
                Question = new();
            }
            else
            {
                Question = await _mediator.Send(new GetQuestionByIdQuery(questionId.Value));
            }
            Question.QuestionCategoryId = questionCategoryId ?? 0;
        }

        public async Task<IActionResult> OnPost(Question question)
        {
            await _mediator.Send(new CreateOrChangeQuestionCommand(question));
            return RedirectToPage("ConcreteQuestionCategory", new { questionCategoryId = question.QuestionCategoryId });
        }
    }
}
