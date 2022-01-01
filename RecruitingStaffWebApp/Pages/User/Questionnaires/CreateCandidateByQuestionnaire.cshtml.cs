using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class CreateCandidateByQuestionnaireModel : BasePageModel
    {
        public CreateCandidateByQuestionnaireModel(IMediator mediator, ILogger<CreateCandidateByQuestionnaireModel> logger) : base(mediator, logger)
        {
        }

        public int QuestionnaireId { get; set; }

        public void OnGet(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public async Task<IActionResult> OnPost(CandidateViewModel candidateViewModel, int questionnaireId)
        {
            var candidateEntity = GetEntity<Candidate, CandidateViewModel>(candidateViewModel);
            await _mediator.Send(new CreateEntityCommand<Candidate>(candidateEntity));
            await _mediator.Send(new CreateMapCommand<CandidateQuestionnaire>(candidateEntity.Id, questionnaireId));
            return RedirectToPage("Candidates", new { questionnaireId });
        }
    }
}
