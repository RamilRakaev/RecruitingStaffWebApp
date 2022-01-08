using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ChangeCandidateModel : BasePageModel
    {
        public ChangeCandidateModel(IMediator mediator, ILogger<ChangeCandidateModel> logger) : base(mediator, logger)
        { }

        public CandidateViewModel CandidateViewModel { get; set; }
        public SelectList Questionnaires { get; set; }

        public async Task OnGet(int candidateId)
        {
            CandidateViewModel = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId))
                );
            Questionnaires = new SelectList(await _mediator.Send(new GetEntitiesQuery<Questionnaire>()), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidateViewModel, int questionnaireId, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var candidateEntity = GetEntity<Candidate, CandidateViewModel>(candidateViewModel);
                await _mediator.Send(new ChangeEntityCommand<Candidate>(candidateEntity));
                await _mediator.Send(new CreateMapCommand<CandidateQuestionnaire>(candidateEntity.Id, questionnaireId));
                await _mediator.Send(new CreateOrChangeFileCommand(formFile, candidateEntity.Name, candidateId: candidateEntity.Id, questionnaireId: questionnaireId));
                return RedirectToPage("ConcreteCandidate", new { CandidateId = candidateViewModel.Id });
            }
            CandidateViewModel = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateViewModel.Id))
                );
            Questionnaires = new SelectList(await _mediator.Send(new GetEntitiesQuery<Questionnaire>()), "Id", "Name");
            return Page();
        }
    }
}
