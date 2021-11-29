using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
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

        public Questionnaire Questionnaire { get; set; }
        public Dictionary<QuestionCategory, Question[]> CategoriesWithQuestions { get; set; }

        public async Task OnGet(int questionnaireId)
        {
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            CategoriesWithQuestions = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }

        public async Task OnPostRemoveQuestion(int questionId, int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionCommand(questionId));
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            CategoriesWithQuestions = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }

        public async Task OnPostRemoveQuestionCategory(int questionCategoryId, int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            CategoriesWithQuestions = await _mediator.Send(new GetConcreteQuestionnaireQuery(questionnaireId));
        }
    }
}
