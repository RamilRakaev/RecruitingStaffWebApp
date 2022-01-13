using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.IO;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ConcreteCandidateModel : BasePageModel
    {
        private readonly WebAppOptions _option;

        public ConcreteCandidateModel(IMediator mediator, ILogger<BasePageModel> logger, IOptions<WebAppOptions> options) : base(mediator, logger)
        {
            _option = options.Value;
        }

        public CandidateOptionsViewModel CandidateOptionsViewModel { get; set; }

        public virtual async Task OnGet(int candidateId)
        {
            CandidateOptionsViewModel = await Initialization(candidateId);
        }

        public async Task OnPost(int optionId, int candidateId)
        {
            await _mediator.Send(new RemoveEntityCommand<Option>(optionId));
            CandidateOptionsViewModel = await Initialization(candidateId);
        }

        public async Task<IActionResult> OnGetReturnImage(int candidateId)
        {
            CandidateOptionsViewModel = await Initialization(candidateId);
            var candidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
            if (candidatePhotoSource != string.Empty)
            {
                string source = _option.GetSource(FileType.JpgPhoto);
                string file_path = Path.Combine(source, candidatePhotoSource);
                string file_type = _option.GetMimeType(FileType.JpgPhoto);
                return PhysicalFile(file_path, file_type, candidatePhotoSource);
            }
            return null;
        }

        protected async virtual Task<CandidateOptionsViewModel> Initialization(int candidateId)
        {
            CandidateOptionsViewModel candidateOptionsViewModel = new();
            var candidate = await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId));
            var candidateConfig = new MapperConfiguration(c => c.CreateMap<Candidate, CandidateViewModel>());
            var candidateMapper = new Mapper(candidateConfig);
            candidateOptionsViewModel.CandidateViewModel = candidateMapper.Map<CandidateViewModel>(candidate);
            var optionEntities = await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId));
            var optionConfig = new MapperConfiguration(c => c.CreateMap<Option, OptionViewModel>());
            var optionMapper = new Mapper(optionConfig);
            candidateOptionsViewModel.OptionViewModels = optionMapper.Map<OptionViewModel[]>(optionEntities);
            return candidateOptionsViewModel;
        }
    }
}
