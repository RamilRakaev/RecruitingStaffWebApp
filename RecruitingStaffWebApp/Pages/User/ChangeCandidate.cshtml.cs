using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;

namespace RecruitingStaffWebApp.Pages.User
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator, ILogger<ChangeCandidateModel> logger) : base(mediator, logger)
        { }

        public Candidate Candidate { get; set; }

        public async Task<IActionResult> OnGet(int CandidateId)
        {
            Candidate = await _mediator.Send(new GetCandidateQuery(CandidateId));
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(Candidate Candidate, IFormFile formFile)
        {
            await _mediator.Send(new ChangeCandidateCommand(Candidate, formFile));
            return RedirectToPage("ConcreteCandidate", new { CandidateId = Candidate.Id });
        }
    }
}
