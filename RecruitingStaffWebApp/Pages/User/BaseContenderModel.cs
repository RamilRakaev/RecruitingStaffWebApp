﻿using CQRS.Queries.Requests.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User
{
    public class BaseContenderModel : PageModel
    {
        protected readonly IMediator _mediator;

        public BaseContenderModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> RightVerification()
        {
            if (await _mediator.Send(new CheckRoleForUserQuery("user")))
            {
                return Page();
            }
            return RedirectToPage("Account/Login");
        }
    }
}
