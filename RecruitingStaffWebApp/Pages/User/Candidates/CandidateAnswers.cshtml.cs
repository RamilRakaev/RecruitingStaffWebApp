using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CandidateAnswersModel : BasePageModel
    {
        public CandidateAnswersModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public AnswerViewModel[] Answers { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            CandidateId = candidateId;
            Answers = GetViewModels<Answer, AnswerViewModel>(
                await _mediator.Send(new GetAnswersByCanidateIdQuery(candidateId))
                );
        }

        public async Task OnPost(int answerId, int candidateId)
        {
            CandidateId = candidateId;
            await _mediator.Send(new RemoveEntityCommand<Answer>(answerId));
            Answers = GetViewModels<Answer, AnswerViewModel>(
                await _mediator.Send(new GetAnswersByCanidateIdQuery(candidateId))
                );
        }
    }
}
