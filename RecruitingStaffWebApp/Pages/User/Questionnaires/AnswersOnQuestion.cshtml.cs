using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswersOnQuestionModel : BasePageModel
    {
        public AnswersOnQuestionModel(IMediator mediator) : base(mediator)
        {
        }

        public Answer[] Answers { get; set; }
        public Question Question { get; set; }

        public async Task<IActionResult> OnGet(int questionId)
        {
            Question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            Answers = await _mediator.Send(new AnswerOnQuestionQuery(questionId));
            return await RightVerification();
        }
    }
}
