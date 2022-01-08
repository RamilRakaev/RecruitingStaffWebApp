using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class QuestionsParseFormModel : BasePageModel
    {
        public QuestionsParseFormModel(IMediator mediator, ILogger<QuestionsParseFormModel> logger) : base(mediator, logger)
        { }

        public SelectList QuestionnaireTypes { get; set; }

        public async Task OnGet()
        {
            QuestionnaireTypes = new SelectList(
                await _mediator.Send(new GetJobQuestionnairesDictionaryQuery()),
                "Key",
                "Value");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, int jobQuestionnaire)
        {
            if (await _mediator.Send(new DocumentParseCommand(formFile, jobQuestionnaire, parseQuestions: true)))
            {
                return RedirectToPage("/User/Candidates/Candidates");
            }
            ModelState.AddModelError("", "Не удалось проанализировать документ");
            QuestionnaireTypes = new SelectList(
               await _mediator.Send(new GetJobQuestionnairesDictionaryQuery()),
               "Key",
               "Value");
            return Page();
        }
    }
}
