using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
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
        public string CandidatePhotoSource { get; set; }

        public async Task OnGet(int candidateId, int questionId)
        {
            QuestionId = questionId;
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsQuery());
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }

        public async Task OnPost(int optionId, int candidateId, int questionId)
        {
            QuestionId = questionId;
            await _mediator.Send(new RemoveOptionCommand(optionId));
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsQuery());
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }
    }
}
