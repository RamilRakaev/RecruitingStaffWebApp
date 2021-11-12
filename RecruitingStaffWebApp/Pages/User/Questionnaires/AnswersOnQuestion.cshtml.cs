using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswersOnQuestionModel : BasePageModel
    {
        public AnswersOnQuestionModel(IMediator mediator, ILogger<AnswersOnQuestionModel> logger) : base(mediator, logger)
        {
        }

        public Answer[] Answers { get; set; }
        public Question Question { get; set; }

        public async Task<IActionResult> OnGet(int questionId)
        {
            Question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            Answers = await _mediator.Send(new AnswersOnQuestionQuery(questionId));
            return await RightVerification();
        }

        public async Task OnPost(int questionId, int answerId)
        {
            await _mediator.Send(new RemoveAnswerCommand(answerId));
            Question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            Answers = await _mediator.Send(new AnswersOnQuestionQuery(questionId));
        }
    }
}
