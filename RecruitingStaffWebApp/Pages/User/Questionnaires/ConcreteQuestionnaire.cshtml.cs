using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class ConcreteQuestionnaireModel : BasePageModel
    {
        public ConcreteQuestionnaireModel(IMediator mediator) : base(mediator)
        {
        }

        public Questionnaire Questionnaire { get; set; }
        public QuestionCategory[] QuestionCategories { get; set; }
        public Question[] Questions { get; set; }

        public async Task<IActionResult> OnGet(int questionnaireId)
        {
            Questionnaire = await _mediator.Send(new GetQuestionnaireQuery(questionnaireId));
            QuestionCategories = await _mediator.Send(new GetQuestionCategoriesByQuestionnaireQuery(Questionnaire.Id));
            Questions = await _mediator.Send(new GetQuestionsByQuestionCategoryQuery(QuestionCategories));
            return await RightVerification();
        }
    }
}
