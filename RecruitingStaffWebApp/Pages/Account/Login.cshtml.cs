using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;

namespace RecruitingStaffWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;

        public LoginModel(IMediator mediator,
            ILogger<LoginModel> logger)
        {
            Login = new UserLoginCommand();
            _mediator = mediator;
            _logger = logger;
        }

        public UserLoginCommand Login { get; set; }

        public IActionResult OnGet()
        {
            var isUser = _mediator.Send(new CheckRoleForUserQuery("user")).Result;
            if (isUser)
            {
                return RedirectToPage("/User/Candidates/Candidates");
            }
            _logger.LogInformation($"Login page visited");
            return Page();
        }

        public async Task<IActionResult> OnPost(UserLoginCommand login)
        {
            if (ModelState.IsValid)
            {
                login.Page = this;
                string message = await _mediator.Send(login);
                ModelState.AddModelError("", message);
                return RedirectToPage("/User/Candidates/Candidates");
            }
            else
            {
                ModelState.AddModelError("", "Неправильная почта и (или) пароль");
                _logger.LogWarning($"Incorrect email and (or) password");
                return Page();
            }
        }
    }
}
