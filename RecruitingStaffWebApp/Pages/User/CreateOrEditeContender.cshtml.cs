using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class CreateOrEditeContenderModel : BaseContenderModel
    {
        public CreateOrEditeContenderModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            return await RightVerification();
        }
    }
}
