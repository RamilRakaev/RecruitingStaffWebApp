using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class CandidatesModel : BasePageModel
    {
        public CandidatesModel(IMediator mediator) : base(mediator)
        {
        }

        public Candidate[] Candidates { get; set; }
        public int QuestionnaireId { get; set; }

        public async Task<IActionResult> OnGet(int questionnaireId)
        {
            Candidates = await _mediator.Send(new GetCandidatesByQuestionnaireQuery(questionnaireId));
            QuestionnaireId = questionnaireId;
            return await RightVerification();
        }

        public async Task OnPost(int questionnaireId, int candidateId)
        {
            await _mediator.Send(new RemoveCandidateCommand(candidateId));
            QuestionnaireId = questionnaireId;
            Candidates = await _mediator.Send(new GetCandidatesByQuestionnaireQuery(questionnaireId));
        }
    }
}