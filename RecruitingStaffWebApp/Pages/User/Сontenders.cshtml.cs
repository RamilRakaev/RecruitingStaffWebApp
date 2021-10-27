using System.Threading.Tasks;
using CQRS.Commands.Requests.Contenders;
using CQRS.Queries.Requests.ApplicationUsers;
using CQRS.Queries.Requests.Contenders;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User
{
    public class СontendersModel : BaseContenderModel
    {
        public СontendersModel(IMediator mediator) : base(mediator)
        { }

        public Contender[] Contenders { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Contenders = await _mediator.Send(new GetContendersQuery());
            return await RightVerification();
        }

        public async Task OnPost(Contender newContender, IFormFile uploadedFile)
        {
            await _mediator.Send(new CreateContenderCommand(newContender, uploadedFile));
            Contenders = await _mediator.Send(new GetContendersQuery());
        }

        public async Task OnPostRemove(int contenderId)
        {
            await _mediator.Send(new RemoveContenderCommand(contenderId));
            Contenders = await _mediator.Send(new GetContendersQuery());
        }
    }
}
