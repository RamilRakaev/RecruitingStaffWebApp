using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;

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
