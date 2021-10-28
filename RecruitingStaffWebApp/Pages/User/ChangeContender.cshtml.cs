using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Commands.Requests.Contenders;
using CQRS.Commands.Requests.Options;
using CQRS.Queries.Requests.Contenders;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ChangeContenderModel : BasePageModel
    {
        public ChangeContenderModel(IMediator mediator) : base(mediator)
        { }

        public Contender Contender { get; set; }

        public async Task<IActionResult> OnGet(int contenderId)
        {
            Contender = await _mediator.Send(new GetContenderQuery(contenderId));
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Contender contender)
        {
            await _mediator.Send(new ChangeContenderCommand(contender));
            return RedirectToPage("ConcreteContender", new { contenderId = contender.Id });
        }
    }
}
