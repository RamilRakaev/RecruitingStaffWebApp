using System.Threading.Tasks;
using CQRS.Commands.Requests.Options;
using CQRS.Queries.Requests.Candidates;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ConcreteCandidateModel : BasePageModel
    {
        public ConcreteCandidateModel(IMediator mediator) : base(mediator)
        { }

        public Candidate Candidate { get; set; } 

        public async Task<IActionResult> OnGet(int candidateId)
        {
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            return await RightVerification();
        }

        public async Task OnPost(int optionId, int candidateId)
        {
            await _mediator.Send(new RemoveOptionCommand(optionId));
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
        }
    }
}
