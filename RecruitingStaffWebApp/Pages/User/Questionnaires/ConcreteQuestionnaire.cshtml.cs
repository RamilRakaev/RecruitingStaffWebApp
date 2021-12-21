using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class ConcreteQuestionnaireModel : BasePageModel
    {
        public ConcreteQuestionnaireModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public QuestionnaireViewModel Questionnaire { get; set; }
        public QuestionCategoryViewModel[] QuestionCategories { get; set; }

        public async Task OnGet(int questionnaireId)
        {
            await Initialize(questionnaireId);
        }

        public async Task OnPostRemoveQuestion(int questionId, int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionCommand(questionId));
            await Initialize(questionnaireId);

        }

        public async Task OnPostRemoveQuestionCategory(int questionCategoryId, int questionnaireId)
        {
            await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategoryId));
            await Initialize(questionnaireId);
        }

        private async Task Initialize(int questionnaireId)
        {
            Questionnaire = GetViewModel<Questionnaire, QuestionnaireViewModel>(
                await _mediator.Send(new GetQuestionnaireQuery(questionnaireId)));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionCategory, QuestionCategoryViewModel>();
                cfg.CreateMap<Question, QuestionViewModel>();
            });
            var mapper = config.CreateMapper();
            QuestionCategories = mapper.Map<QuestionCategoryViewModel[]>(
                await _mediator.Send(new GetQuestionCategoriesWithQuestionsQuery(questionnaireId)));
        }
    }
}
