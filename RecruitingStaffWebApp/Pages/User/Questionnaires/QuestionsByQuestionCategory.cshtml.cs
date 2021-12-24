using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionsByQuestionCategoryModel : BasePageModel
    {
        public QuestionsByQuestionCategoryModel(IMediator mediator, ILogger<QuestionsByQuestionCategoryModel> logger) : base(mediator, logger)
        {
        }

        public QuestionViewModel[] Questions { get; set; }
        public AnswerViewModel[] Answers { get; set; }
        public QuestionCategoryViewModel QuestionCategory { get; set; }

        public async Task Initialize(int questionCategoryId)
        {
            QuestionCategory = GetViewModel<QuestionCategory, QuestionCategoryViewModel>(
                await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId))
                );
            Questions = GetViewModels<Question, QuestionViewModel>(
                await _mediator.Send(new GetQuestionsByCategoryIdQuery(questionCategoryId)));
            Answers = GetViewModels<Answer, AnswerViewModel>(
                await _mediator.Send(new GetAnswersByQuestionCategoryQuery(questionCategoryId)));
        }

        public async Task OnGet(int questionCategoryId, int questionId)
        {
            if (questionCategoryId != 0)
            {
                await Initialize(questionCategoryId);
            }
            else
            {
                var question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
                await Initialize(question.QuestionCategoryId);
            }
        }

        public async Task OnPostRemoveQuestion(int questionCategoryId, int questionId)
        {
            await _mediator.Send(new RemoveQuestionCommand(questionId));
            await Initialize(questionCategoryId);
        }

        public async Task OnPostRemoveAnswer(int questionId, int answerId)
        {
            await _mediator.Send(new RemoveEntityCommand<Answer>(answerId));
            var question = await _mediator.Send(new GetQuestionByIdQuery(questionId));
            await Initialize(question.QuestionCategoryId);
        }
    }
}
