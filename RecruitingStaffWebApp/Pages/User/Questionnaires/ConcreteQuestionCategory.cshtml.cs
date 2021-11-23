using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class ConcreteQuestionCategoryModel : BasePageModel
    {
        public ConcreteQuestionCategoryModel(IMediator mediator, ILogger<ConcreteQuestionCategoryModel> logger) : base(mediator, logger)
        {
        }

        public Question[] Questions { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public async Task OnGet(int questionCategoryId)
        {
            Questions = await _mediator.Send(new GetQuestionsByCategoryIdQuery(questionCategoryId));
            QuestionCategory = await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId));
        }

        public async Task OnPost(int questionCategoryId, int questionId)
        {
            await _mediator.Send(new RemoveQuestionCommand(questionId));
            Questions = await _mediator.Send(new GetQuestionsByCategoryIdQuery(questionCategoryId));
            QuestionCategory = await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId));
        }
    }
}
