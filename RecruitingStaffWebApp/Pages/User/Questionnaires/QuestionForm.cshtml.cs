using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionFormModel : BasePageModel
    {
        public QuestionFormModel(IMediator mediator, ILogger<QuestionFormModel> logger) : base(mediator, logger)
        {
        }

        public int QuestionnaireId { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
        public SelectList QuestionCategories { get; set; }

        public async Task OnGet(int? questionId, int? questionCategoryId, int questionnaireId)
        {
            await Initialize(questionnaireId);
            if (questionId == null)
            {
                QuestionViewModel = new() { QuestionCategoryId = questionCategoryId ?? 0 };
            }
            else
            {
                var question = await _mediator.Send(new GetEntityByIdQuery<Question>(questionId.Value));
                var config = new MapperConfiguration(c => c.CreateMap<Question, QuestionViewModel>());
                var mapper = new Mapper(config);
                QuestionViewModel = mapper.Map<QuestionViewModel>(question);
            }
        }

        public async Task<IActionResult> OnPost(QuestionViewModel questionViewModel, int questionnaireId)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<QuestionViewModel, Question>());
                var mapper = new Mapper(config);
                var questionEntity = mapper.Map<Question>(questionViewModel);
                if (questionViewModel.Id == 0)
                {
                    await _mediator.Send(new CreateEntityCommand<Question>(questionEntity));
                }
                else
                {
                    await _mediator.Send(new ChangeEntityCommand<Question>(questionEntity));
                }
                return RedirectToPage("ConcreteQuestionnaire", new { questionnaireId });
            }
            ModelState.AddModelError("", "Неправильно введены данные");
            await Initialize(questionnaireId);
            QuestionViewModel = questionViewModel;
            return Page();
        }

        private async Task Initialize(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
            QuestionCategories = new SelectList(
             await _mediator
             .Send(
                 new GetQuestionCategoriesByQuestionnaireIdQuery(QuestionnaireId)),
             "Id",
             "Name");
        }
    }
}
