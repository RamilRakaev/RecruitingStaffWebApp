using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class QuestionnaireExampleParseModel : BasePageModel
    {
        public QuestionnaireExampleParseModel(IMediator mediator, ILogger<QuestionnaireExampleParseModel> logger) : base(mediator, logger)
        { }

        //public SelectList QuestionnaireTypes { get; set; }

        public void OnGet()
        {
            //QuestionnaireTypes = new SelectList(
            //    await _mediator.Send(new GetJobQuestionnairesDictionaryQuery()),
            //    "Key",
            //    "Value");
        }

        public async Task<IActionResult> OnPost(IFormFile formFile)
        {
            if (await _mediator.Send(new DocumentParseCommand(formFile, false)))
            {
                return RedirectToPage("/User/Candidates/Candidates");
            }
            ModelState.AddModelError("", "Не удалось проанализировать документ");
            //QuestionnaireTypes = new SelectList(
            //   await _mediator.Send(new GetJobQuestionnairesDictionaryQuery()),
            //   "Key",
            //   "Value");
            return Page();
        }
    }
}
