using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class AnswersOnQuestionModel : BasePageModel
    {
        public AnswersOnQuestionModel(IMediator mediator, ILogger<AnswersOnQuestionModel> logger) : base(mediator, logger)
        {
        }

        public int QuestionnaireId { get; set; }
        public AnswerViewModel[] Answers { get; set; }
        public QuestionViewModel Question { get; set; }

        public async Task OnGet(int questionId, int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            Question = GetViewModel<Question, QuestionViewModel>(
                await _mediator.Send(new GetQuestionByIdQuery(questionId)));
            Answers = GetViewModels<Answer, AnswerViewModel>(
                await _mediator.Send(new AnswersOnQuestionQuery(questionId)));
        }

        public async Task OnPost(int questionnaireId, int questionId, int answerId)
        {
            QuestionnaireId = questionnaireId;
            await _mediator.Send(new RemoveEntityCommand<Answer>(answerId));
            Question = GetViewModel<Question, QuestionViewModel>(
                await _mediator.Send(new GetQuestionByIdQuery(questionId)));
            Answers = GetViewModels<Answer, AnswerViewModel>(
                await _mediator.Send(new AnswersOnQuestionQuery(questionId)));
        }
    }
}
