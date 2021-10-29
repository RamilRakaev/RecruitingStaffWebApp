using System.Threading.Tasks;
using CQRS.Commands.Requests.Candidates;
using CQRS.Queries.Requests.Candidates;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecruitingStaffWebApp.Pages.User
{
    public class CandidatesModel : BasePageModel
    {
        public CandidatesModel(IMediator mediator) : base(mediator)
        { }

        public Candidate[] Candidates { get; set; }
        public string MessageAboutDocumentsSource { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Candidates = await _mediator.Send(new GetCandidatesQuery());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceCommand());
            return await RightVerification();
        }

        public async Task OnPost(Candidate newCandidate, IFormFile uploadedFile)
        {
            await _mediator.Send(new CreateCandidateCommand(newCandidate, uploadedFile));
            Candidates = await _mediator.Send(new GetCandidatesQuery());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceCommand());
        }

        public async Task OnPostRemove(int CandidateId)
        {
            await _mediator.Send(new RemoveCandidateCommand(CandidateId));
            Candidates = await _mediator.Send(new GetCandidatesQuery());
            MessageAboutDocumentsSource = await _mediator.Send(new CheckDocumentsSourceCommand());
        }
    }
}
