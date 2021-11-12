using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RecruitingStaffWebApp.Pages.User
{
    public class BasePageModel : PageModel
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<BasePageModel> _logger;

        public BasePageModel(IMediator mediator, ILogger<BasePageModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected async Task<IActionResult> RightVerification()
        {
            if (await _mediator.Send(new CheckRoleForUserQuery("user")))
            {
                _logger.LogInformation("Validation passed");
                return Page();
            }
            _logger.LogWarning("Unauthorized login attempt!");
            return RedirectToPage("Account/Login");
        }
    }
}
