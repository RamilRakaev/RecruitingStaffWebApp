using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.ApplicationUsers
{
    public class ApplicationUserFormModel : BasePageModel
    {
        public ApplicationUserFormModel(IMediator mediator, ILogger<ApplicationUserFormModel> logger) : base(mediator, logger)
        {
        }

        public CreateOrChangeUserCommand AppUser { get; set; }

        public async Task<IActionResult> OnGet(int? userId)
        {
            AppUser = new CreateOrChangeUserCommand();
            if (userId != null)
            {
                AppUser.Id = userId.Value;
            }
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(CreateOrChangeUserCommand appUser)
        {
            await _mediator.Send(appUser);
            return RedirectToPage("ApplicationUsers");
        }
    }
}
