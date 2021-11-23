using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ConcreteCandidateModel : BasePageModel
    {
        public ConcreteCandidateModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public Candidate Candidate { get; set; }
        public Option[] Options { get; set; }
        public string CandidatePhotoSource { get; set; }

        public async Task OnGet(int candidateId)
        {
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId));
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }

        public async Task OnPost(int optionId, int candidateId)
        {
            await _mediator.Send(new RemoveOptionCommand(optionId));
            Candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            Options = await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId));
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }
    }
}
