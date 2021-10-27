using CQRS.Queries.Requests.ApplicationUsers;
using Domain.Model.UserIdentity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager, IMediator mediator)
        {
            _logger = logger;
            signInManager.SignOutAsync();
            _mediator = mediator;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("Index page visited");
            if (await _mediator.Send(new CheckRoleForUserQuery("user")))
            {
                return Page();
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
