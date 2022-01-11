using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
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

        public QuestionnaireViewModel QuestionnaireViewModel { get; set; }
        public QuestionCategoryViewModel[] QuestionCategoryViewModels { get; set; }

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
            var questionnaireEntity =
                await _mediator.Send(new GetEntityByIdQuery<Questionnaire>(questionnaireId));
            var questionnaireConfig = new MapperConfiguration(
                c => c.CreateMap<Questionnaire, QuestionnaireViewModel>());
            var questionnaireMapper = new Mapper(questionnaireConfig);
            QuestionnaireViewModel = questionnaireMapper.Map<QuestionnaireViewModel>(questionnaireEntity);
            var questionCategoiesConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<QuestionCategory, QuestionCategoryViewModel>();
                c.CreateMap<Question, QuestionViewModel>();
            });
            var questionCategoriesMapper = questionCategoiesConfig.CreateMapper();
            QuestionCategoryViewModels = questionCategoriesMapper.Map<QuestionCategoryViewModel[]>(
                await _mediator.Send(new GetQuestionCategoriesWithQuestionsQuery(questionnaireId)));
        }
    }
}
