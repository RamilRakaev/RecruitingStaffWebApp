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
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswerAuthorModel : BasePageModel
    {
        public AnswerAuthorModel(IMediator mediator, ILogger<AnswerAuthorModel> logger) : base(mediator, logger)
        { }

        public CandidateViewModel Candidate { get; set; }
        public OptionViewModel[] OptionViewModels { get; set; }
        public int QuestionId { get; set; }
        public string CandidatePhotoSource { get; set; }
        public int QuestionnaireId { get; set; }

        public async Task OnGet(int candidateId, int questionId, int questionnaireId)
        {
            await Initialize(candidateId, questionId, questionnaireId);
        }

        public async Task OnPost(int optionId, int candidateId, int questionId, int questionnaireId)
        {
            await _mediator.Send(new RemoveEntityCommand<Option>(optionId));
            await Initialize(candidateId, questionId, questionnaireId);
        }

        private async Task Initialize(int candidateId,int questionId, int questionnaireId)
        {
            QuestionId = questionId;
            var candidateEntity = await _mediator.Send(new GetEntityByIdQuery<Candidate>(candidateId));
            var config = new MapperConfiguration(c => c.CreateMap<Candidate, CandidateViewModel>());
            var mapper = new Mapper(config);
            Candidate = mapper.Map<CandidateViewModel>(candidateEntity);

            var optionEntities = await _mediator.Send(new GetOptionsByCandidateIdQuery(candidateId));
            var optionconfig = new MapperConfiguration(c => c.CreateMap<Option, OptionViewModel>());
            var optionMapper = new Mapper(config);
            OptionViewModels = mapper.Map<OptionViewModel[]>(optionEntities);
            CandidatePhotoSource = await _mediator.Send(new GetSourceOfCandidatePhotoQuery(candidateId));
            QuestionnaireId = questionnaireId;
        }
    
    }
}
