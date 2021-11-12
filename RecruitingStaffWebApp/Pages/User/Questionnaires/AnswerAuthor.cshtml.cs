using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswerAuthorModel : BasePageModel
    {
        public AnswerAuthorModel(IMediator mediator, ILogger<AnswerAuthorModel> logger) : base(mediator, logger)
        { }

        public Candidate Candidate { get; set; }
        public Option[] Options { get; set; }
        public int QuestionId { get; set; }

        public async Task<IActionResult> OnGet(int candidateId, int questionId)
        {
            QuestionId = questionId;
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsQuery());
            return await RightVerification();
        }

        public async Task OnPost(int optionId, int candidateId, int questionId)
        {
            QuestionId = questionId;
            await _mediator.Send(new RemoveOptionCommand(optionId));
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsQuery());
        }
    }
}
