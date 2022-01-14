using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.CandidateQuestionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps;
using RecruitingStaff.WebApp.ViewModels.Maps;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.CandidateQuestionnaires
{
    public class CandidateQuestionnairesModel : BasePageModel
    {
        public CandidateQuestionnairesModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public CandidateQuestionnaireViewModel[] CandidateQuestionnaireViewModels { get; set; }
        public SelectList AvailableVacancies { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            await Initialization(candidateId);
        }

        public async Task OnPostCreateCandidateQuestionnaire(int candidateId, int questionnaireId)
        {
            await _mediator.Send(new CreateMapCommand<CandidateQuestionnaire>(candidateId, questionnaireId));
            await Initialization(candidateId);
        }

        public async Task OnPost(int candidateId, int candidateQuestionnaireId)
        {
            await _mediator.Send(new RemoveEntityCommand<CandidateQuestionnaire>(candidateQuestionnaireId));
            await Initialization(candidateId);
        }

        private async Task Initialization(int candidateId)
        {
            CandidateId = candidateId;
            var candidatqQuestionnaireEntities =
                await _mediator.Send(new GetMapsByFirstEntityIdQuery<CandidateQuestionnaire>(candidateId));
            var config = new MapperConfiguration(c => c.CreateMap<CandidateQuestionnaire, CandidateQuestionnaireViewModel>());
            var mapper = new Mapper(config);
            CandidateQuestionnaireViewModels = mapper.Map<CandidateQuestionnaireViewModel[]>(candidatqQuestionnaireEntities);
            foreach (var candidateQuestionnaire in CandidateQuestionnaireViewModels)
            {
                var questionnaireEntity =
                    await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(candidateQuestionnaire.SecondEntityId));
                candidateQuestionnaire.QuestionnaireName = questionnaireEntity.Name;
            }
            AvailableVacancies = new(await _mediator.Send(new GetAvailableQuestionnairesQuery(candidateId)), "Id", "Name");
        }
    }
}
