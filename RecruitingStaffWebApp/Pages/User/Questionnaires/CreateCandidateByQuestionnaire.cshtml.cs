using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class CreateCandidateByQuestionnaireModel : BasePageModel
    {
        public CreateCandidateByQuestionnaireModel(IMediator mediator) : base(mediator)
        {
        }

        public int QuestionnaireId { get; set; }

        public void OnGet(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public async Task<IActionResult> OnPost(Candidate candidate, int questionnaireId)
        {
            await _mediator.Send(new CreateCandidateByQuestionnaireCommand(candidate, questionnaireId));
            return RedirectToPage("Candidates");
        }
    }
}
