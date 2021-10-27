using System.Threading.Tasks;
using CQRS.Queries.Requests.Contenders;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class СontendersModel : PageModel
    {
        private readonly IMediator _mediator;

        public СontendersModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Contender[] Contenders { get; set; }

        public async Task OnGet()
        {
            Contenders = await _mediator.Send(new GetContendersQuery());
        }
    }
}
