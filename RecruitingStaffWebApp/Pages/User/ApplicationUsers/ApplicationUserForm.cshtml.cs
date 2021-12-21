using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.UserIdentity;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using RecruitingStaff.WebApp.ViewModels.ApplicationUser;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.ApplicationUsers
{
    public class ApplicationUserFormModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationUsersModel> _logger;

        public ApplicationUserFormModel(IMediator mediator, ILogger<ApplicationUsersModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public ApplicationUserViewModel AppUser { get; set; }

        public async Task OnGet(int? userId)
        {
            AppUser = new ApplicationUserViewModel();
            if (userId != null)
            {
                _logger.LogInformation("Change user");
                AppUser.Id = userId.Value;
                AppUser.Email = await _mediator.Send(new GetEmailQuery(userId.Value));
            }
        }

        public async Task<IActionResult> OnPost(ApplicationUserViewModel appUser)
        {
            if (ModelState.IsValid)
            {
                var command = GetAppUser(appUser);
                var result = await _mediator.Send(command);
                if (result.Succeeded)
                {
                    return RedirectToPage("ApplicationUsers");
                }
                _logger.LogInformation("Input error");
                ModelState.AddModelError("", result.Errors.ElementAt(0).Description);
            }
            AppUser = appUser;
            return Page();
        }

        private static CreateOrChangeUserCommand GetAppUser(ApplicationUserViewModel users)
        {
            var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<ApplicationUserViewModel, CreateOrChangeUserCommand>());
            var mapper = new Mapper(config);
            return mapper.Map<CreateOrChangeUserCommand>(users);
        }
    }
}
