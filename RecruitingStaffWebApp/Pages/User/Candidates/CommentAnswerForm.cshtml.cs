using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CommentAnswerFormModel : BasePageModel
    {
        public CommentAnswerFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public AnswerViewModel Answer { get; set; }

        public void OnGet(int answerId, int candidateId)
        {
            Answer = new()
            {
                Id = answerId,
                CandidateId = candidateId
            };
        }

        public async Task<IActionResult> OnPost(AnswerViewModel answer)
        {
            var answerEntity = GetEntity<Answer, AnswerViewModel>(answer);
            await _mediator.Send(new CommentAnswerCommand(answerEntity));
            return RedirectToPage("CandidateAnswers", new { candidateId = answerEntity.CandidateId });
        }
    }
}
