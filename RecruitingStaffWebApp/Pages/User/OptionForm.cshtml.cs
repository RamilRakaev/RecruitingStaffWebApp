using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Commands.Requests.Options;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator) : base(mediator)
        {
            Option = new Option();
        }

        public Option Option { get; set; }

        public async Task<IActionResult> OnGet(int? contenderId, string propertyName = "", string value = "")
        {
            Option.PropertyName = propertyName;
            Option.ContenderId = contenderId;
            Option.Value = value;
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Option option)
        {
            await _mediator.Send(new CreateOrEditOptionCommand(option));
            if(option.ContenderId != null)
            {
                return RedirectToPage("/User/ConcreteContender", new { contenderId = option.ContenderId.Value });
            }
            return RedirectToPage("/User/Ñontenders");
        }
    }
}
