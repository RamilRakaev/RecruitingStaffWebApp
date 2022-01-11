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

namespace RecruitingStaff.WebApp.Pages.User.Candidates
{
    public class CandidateAnswersModel : BasePageModel
    {
        public CandidateAnswersModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public AnswerViewModel[] Answers { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            await Initialization(candidateId);
        }

        public async Task OnPost(int answerId, int candidateId)
        {
            await _mediator.Send(new RemoveEntityCommand<Answer>(answerId));
            await Initialization(candidateId);
        }

        private async Task Initialization(int candidateId)
        {
            CandidateId = candidateId;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerViewModel>());
            var mapper = new Mapper(config);
            var answerEntities = await _mediator.Send(new GetAnswersByCanidateIdQuery(candidateId));
            Answers = mapper.Map<AnswerViewModel[]>(answerEntities);
            foreach (var answer in Answers)
            {
                var question = await _mediator.Send(new GetEntityByIdQuery<Question>(answer.QuestionId));
                answer.QuestionName = question.Name;
            }
        }
    }
}
