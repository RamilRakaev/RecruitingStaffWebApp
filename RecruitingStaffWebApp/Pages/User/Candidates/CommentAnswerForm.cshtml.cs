using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CommentAnswerFormModel : BasePageModel
    {
        public CommentAnswerFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public CommentAnswerCommand CommentAnswer { get; set; }

        public void OnGet(int answerId, int candidateId)
        {
            CommentAnswer = new CommentAnswerCommand(answerId, candidateId);
        }

        public async Task<IActionResult> OnPost(CommentAnswerCommand commentAnswer)
        {
            await _mediator.Send(commentAnswer);
            return RedirectToPage("CandidateAnswers", new { candidateId = commentAnswer.CandidateId });
        }
    }
}
