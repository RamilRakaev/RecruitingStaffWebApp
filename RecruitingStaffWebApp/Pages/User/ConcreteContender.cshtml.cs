using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Queries.Requests.Contenders;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ConcreteContenderModel : PageModel
    {
        private readonly IMediator _mediator;

        public ConcreteContenderModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Contender Contender { get; set; } 

        public async Task OnGet(int contenderId)
        {
            Contender = await _mediator.Send(new GetContenderQuery(contenderId));
        }
    }
}
