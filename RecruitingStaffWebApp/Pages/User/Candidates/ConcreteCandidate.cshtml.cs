using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Options;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mime;
using RecruitingStaff.Domain.Model;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.Options;

namespace RecruitingStaffWebApp.Pages.User.Candidates
{
    public class ConcreteCandidateModel : BasePageModel
    {
        private readonly WebAppOptions _option;

        public ConcreteCandidateModel(IMediator mediator, ILogger<BasePageModel> logger, IOptions<WebAppOptions> options) : base(mediator, logger)
        {
            _option = options.Value;
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

        public async Task<IActionResult> OnGetReturnImage(int candidateId)
        {
            await Initialize(candidateId);
            if (CandidatePhotoSource != string.Empty)
            {
                string source = _option.GetSource(FileType.JpgPhoto);
                string file_path = Path.Combine(source, CandidatePhotoSource);
                string file_type = _option.GetMimeType(FileType.JpgPhoto);
                return PhysicalFile(file_path, file_type, CandidatePhotoSource);
            }
            return null;
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
