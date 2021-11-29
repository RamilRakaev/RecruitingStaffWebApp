using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaffWebApp.Pages.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class ConcreteQuestionnaireModel : BasePageModel
    {
        public ConcreteQuestionnaireModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public Dictionary<QuestionCategory, Question[]> Questionnaire { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }

        public async Task OnGet(int questionnaireId, string questionnaireName)
        {
            QuestionnaireId = questionnaireId;
            QuestionnaireName = questionnaireName;
            Questionnaire = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }

        public async Task OnPostRemoveQuestion(int questionId, int questionnaireId, string questionnaireName)
        {
            await _mediator.Send(new RemoveQuestionCommand(questionId));
            QuestionnaireId = questionnaireId;
            QuestionnaireName = questionnaireName;
            Questionnaire = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }

        public async Task OnPostRemoveQuestionCategory(int questionCategoryId, int questionnaireId, string questionnaireName)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            QuestionnaireId = questionnaireId;
            QuestionnaireName = questionnaireName;
            Questionnaire = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }
    }
}
