using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.EducationOfCandidate
{
    public class EducationOfCandidateModel : BasePageModel
    {
        public EducationOfCandidateModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public EducationViewModel[] EducationViewModels { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            EducationViewModels = await Initialization(candidateId);
        }

        public async Task OnPost(int candidateId, int educationId)
        {
            await _mediator.Send(new RemoveEntityCommand<Education>(educationId));
            EducationViewModels = await Initialization(candidateId);
        }

        private async Task<EducationViewModel[]> Initialization(int candidateId)
        {
            CandidateId = candidateId;
            var educationEntities = await _mediator.Send(new GetCandidateDataEntitiesByIdQuery<Education>(candidateId));
            var config = new MapperConfiguration(c => c.CreateMap<Education, EducationViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<EducationViewModel[]>(educationEntities);
        }
    }
}
