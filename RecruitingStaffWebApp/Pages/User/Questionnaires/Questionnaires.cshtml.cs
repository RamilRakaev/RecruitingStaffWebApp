using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionnairesModel : BasePageModel
    {
        public QuestionnairesModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public string MessageAboutDocumentsSource { get; set; }
        public QuestionnaireViewModel[] Questionnaires { get; set; }

        public async Task OnGet(string messageAboutDocumentsSource)
        {
            MessageAboutDocumentsSource = messageAboutDocumentsSource;
            await Initial();
        }

        public async Task OnPostRemoveQuestionnaire(int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionnaireCommand(questionnaireId));
            await Initial();
        }

        public async Task OnPostRemoveQuestionCategory(int questionCategoryId)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            RemoveLog("QuestionCategory", questionCategoryId);
            await Initial();
        }

        private async Task Initial()
        {
            var questionnaireEntities = await _mediator.Send(new GetEntitiesQuery<Questionnaire>());
            var config = new MapperConfiguration(c => c.CreateMap<Questionnaire, QuestionnaireViewModel>());
            var mapper = new Mapper(config);
            Questionnaires =  mapper.Map<QuestionnaireViewModel[]>(questionnaireEntities);
        }
    }
}
