using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
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
            await _mediator.Send(new CreateCandidateByQuestionnaireCommand(
                GetEntity<Candidate, CandidateViewModel>(candidateViewModel),
                questionnaireId));
            return RedirectToPage("Candidates", new { questionnaireId });
        }
    }
}
