using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
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

        public async Task<IActionResult> OnGet(int questionnaireId)
        {
            Candidates = await _mediator.Send(new GetCandidatesByQuestionnaireQuery(questionnaireId));
            return await RightVerification();
        }
    }
}
