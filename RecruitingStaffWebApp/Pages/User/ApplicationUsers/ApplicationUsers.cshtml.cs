using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Request.ApplicationUsers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using RecruitingStaff.WebApp.ViewModels.ApplicationUser;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.ApplicationUsers
{
    public class ApplicationUsersModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationUsersModel> _logger;

        public ApplicationUsersModel(IMediator mediator, ILogger<ApplicationUsersModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public ApplicationUserViewModel[] ApplicationUsers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (await _mediator.Send(new CheckRoleForUserQuery("user")))
            {
                _logger.LogInformation("Validation passed");
                ApplicationUsers = GetViewModels(
                    await _mediator.Send(new GetUsersQuery()));
                return Page();
            }
            _logger.LogWarning("Unauthorized login attempt!");
            return RedirectToPage("Account/Login");
        }

        public async Task OnPost(int userId)
        {
            await _mediator.Send(new RemoveUserCommand(userId));
            ApplicationUsers = GetViewModels(
                await _mediator.Send(new GetUsersQuery()));
        }

        private static ApplicationUserViewModel[] GetViewModels(ApplicationUser[] users)
        {
            var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<ApplicationUserViewModel[]>(users);
        }
    }
}
