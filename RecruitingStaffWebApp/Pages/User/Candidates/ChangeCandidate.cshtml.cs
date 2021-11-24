using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator, ILogger<ChangeCandidateModel> logger) : base(mediator, logger)
        { }

        public Candidate Candidate { get; set; }
        public SelectList Questionnaires { get; set; }

        public async Task OnGet(int CandidateId)
        {
            Candidate = await _mediator.Send(new GetCandidateQuery(CandidateId));
            Questionnaires = new SelectList(await _mediator.Send(new GetQuestionnairesQuery()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(Candidate Candidate, int questionnaireId, IFormFile formFile)
        {
            await _mediator.Send(new ChangeCandidateCommand(Candidate, formFile, questionnaireId));
            return RedirectToPage("ConcreteCandidate", new { CandidateId = Candidate.Id });
        }
    }
}
