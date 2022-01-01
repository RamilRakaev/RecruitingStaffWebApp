using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class AnswerEvaluationFormModel : BasePageModel
    {
        public AnswerEvaluationFormModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public AnswerViewModel AnswerViewModel { get; set; }

        public async Task OnGet(int answerId)
        {
            var answerEntity = await _mediator.Send(new GetEntityByIdQuery<Answer>(answerId));
            AnswerViewModel = GetViewModel<Answer, AnswerViewModel>(answerEntity);
        }

        public async Task<IActionResult> OnPost(AnswerViewModel answerViewModel)
        {
            var answerEntity = GetEntity<Answer, AnswerViewModel>(answerViewModel);
            await _mediator.Send(new ChangeEntityCommand<Answer>(answerEntity));
            return RedirectToPage("CandidateAnswers", new { candidateId = answerEntity.CandidateId });
        }
    }
}
