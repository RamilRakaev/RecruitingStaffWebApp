using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
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
            await _mediator.Send(new RemoveEntityCommand<Option>(optionId));
            await Initialize(candidateId);
        }

        private async Task Initialize(int candidateId)
        {
            var candidate = await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId));
            var candidateConfig = new MapperConfiguration(c => c.CreateMap<Candidate, CandidateViewModel>());
            var candidateMapper = new Mapper(candidateConfig);
            Candidate = candidateMapper.Map<CandidateViewModel>(candidate);
            var optionEntities = await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId));
            var optionConfig = new MapperConfiguration(c => c.CreateMap<Option, OptionViewModel>());
            var optionMapper = new Mapper(optionConfig);
            Options = optionMapper.Map<OptionViewModel[]>(optionEntities);
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
        }
    }
}
