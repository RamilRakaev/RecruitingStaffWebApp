using System.Threading.Tasks;
using CQRS.Commands.Requests.Candidates;
using CQRS.Queries.Requests.Candidates;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator) : base(mediator)
        { }

        public Candidate Candidate { get; set; }

        public async Task<IActionResult> OnGet(int CandidateId)
        {
            Candidate = await _mediator.Send(new GetCandidateQuery(CandidateId));
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Candidate Candidate)
        {
            await _mediator.Send(new ChangeCandidateCommand(Candidate));
            return RedirectToPage("ConcreteCandidate", new { CandidateId = Candidate.Id });
        }
    }
}
