using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator, ILogger<ChangeCandidateModel> logger) : base(mediator, logger)
        { }

        public CandidateViewModel Candidate { get; set; }
        public SelectList Questionnaires { get; set; }

        public async Task OnGet(int candidateId)
        {
            Candidate = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId))
                );
            Questionnaires = new SelectList(await _mediator.Send(new GetQuestionnairesQuery()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidate, int questionnaireId, IFormFile formFile)
        {
            var candidateEntity = GetEntity<Candidate, CandidateViewModel>(candidate);
            await _mediator.Send(new ChangeCandidateCommand(candidateEntity, formFile, questionnaireId));
            return RedirectToPage("ConcreteCandidate", new { CandidateId = candidate.Id });
        }
    }
}
