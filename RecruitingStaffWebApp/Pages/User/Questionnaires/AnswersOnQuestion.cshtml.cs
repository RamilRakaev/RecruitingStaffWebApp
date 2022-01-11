using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
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
        public QuestionViewModel QuestionViewModel { get; set; }

        public async Task OnGet(int questionId, int questionnaireId)
        {
            await Initialize(questionnaireId, questionId);
        }

        public async Task OnPost(int questionnaireId, int questionId, int answerId)
        {
            await _mediator.Send(new RemoveEntityCommand<Answer>(answerId));
            await Initialize(questionnaireId, questionId);
        }

        private async Task Initialize(int questionnaireId, int questionId)
        {
            QuestionnaireId = questionnaireId;
            var question = await _mediator.Send(new GetEntityByIdQuery<Question>(questionId));
            var config = new MapperConfiguration(c => c.CreateMap<Question, QuestionViewModel>());
            var mapper = new Mapper(config);
            QuestionViewModel = mapper.Map<QuestionViewModel>(question);
            var answers = await _mediator.Send(new AnswersOnQuestionQuery(questionId));
            var answerConfig = new MapperConfiguration(c => c.CreateMap<Answer, AnswerViewModel>());
            var answerMapper = new Mapper(answerConfig);
            Answers = answerMapper.Map<AnswerViewModel[]>(question);
        }
    }
}
