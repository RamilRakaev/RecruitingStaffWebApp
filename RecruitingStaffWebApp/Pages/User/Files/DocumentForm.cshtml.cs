using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class DocumentFormModel : BasePageModel
    {
        public DocumentFormModel(IMediator mediator, ILogger<DocumentFormModel> logger) : base(mediator, logger)
        { }

        public async Task<IActionResult> OnGet()
        {
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(IFormFile formFile)
        {
            await _mediator.Send(new DocumentParseCommand(formFile));
            return RedirectToPage("/User/Candidates/Candidates");
        }
    }
}
