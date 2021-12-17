using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels;

namespace RecruitingStaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            mediator.Send(new CreateOrChangeByViewModelCommand(new CandidateViewModel() {Name = "name" }));
        }

        public IActionResult OnGet()
        {
            _logger.LogInformation("Index page visited");
            return RedirectToPage("/Account/Login");
        }
    }
}
