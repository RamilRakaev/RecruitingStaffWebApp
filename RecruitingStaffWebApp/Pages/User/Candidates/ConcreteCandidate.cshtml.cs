using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ConcreteCandidateModel : BasePageModel
    {
        public ConcreteCandidateModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public CandidateViewModel Candidate { get; set; }
        public OptionViewModel[] Options { get; set; }
        public string CandidatePhotoSource { get; set; }

        public async Task OnGet(int candidateId)
        {
            await Initialize(candidateId);
        }

        public async Task OnPost(int optionId, int candidateId)
        {
            await _mediator.Send(new RemoveOptionCommand(optionId));
            await Initialize(candidateId);
        }

        private async Task Initialize(int candidateId)
        {
            Candidate = GetViewModel<Candidate, CandidateViewModel>(
                await _mediator.Send(new GetCandidateQuery(candidateId))
                );
            Options = GetViewModels<Option, OptionViewModel>(
                await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId))
                );
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }
    }
}
