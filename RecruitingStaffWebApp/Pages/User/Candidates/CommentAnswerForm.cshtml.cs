using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CommentAnswerFormModel : BasePageModel
    {
        public CommentAnswerFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public CommentAnswerCommand CommentAnswer { get; set; }

        public async Task<IActionResult> OnGet(int answerId, int candidateId)
        {
            CommentAnswer = new CommentAnswerCommand(answerId, candidateId);
            return await RightVerification();
        }

        public async Task<IActionResult> OnPost(CommentAnswerCommand commentAnswer)
        {
            await _mediator.Send(commentAnswer);
            return RedirectToPage("CandidateAnswers", new { candidateId = commentAnswer.CandidateId });
        }
    }
}
