using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CandidateAnswersModel : BasePageModel
    {
        public CandidateAnswersModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public Answer[] Answers { get; set; }

        public async Task<IActionResult> OnGet(int candidateId)
        {
            Answers = await _mediator.Send(new GetAnswersByCanidateIdQuery(candidateId));
            return await RightVerification();
        }

        public async Task OnPost(int answerId, int candidateId)
        {
            await _mediator.Send(new RemoveAnswerCommand(answerId));
            Answers = await _mediator.Send(new GetAnswersByCanidateIdQuery(candidateId));
        }
    }
}
