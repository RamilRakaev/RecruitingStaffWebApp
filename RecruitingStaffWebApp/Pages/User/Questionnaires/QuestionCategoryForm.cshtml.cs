using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;
using RecruitingStaffWebApp.Pages.User;
using System;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Questionnaires
{
    public class QuestionCategoryFormModel : BasePageModel
    {
        public QuestionCategoryFormModel(IMediator mediator, ILogger<QuestionCategoryFormModel> logger) : base(mediator, logger)
        {
        }

        public QuestionCategoryViewModel QuestionCategoryViewModel { get; set; }
        public QuestionnaireViewModel[] Questionnaires { get; set; }

        public async Task OnGet(int? questionCategoryId, int? questionnaireId)
        {
            Questionnaires = Array.Empty<QuestionnaireViewModel>();
            if (questionCategoryId == null)
            {
                QuestionCategoryViewModel = new QuestionCategoryViewModel()
                {
                    QuestionnaireId = questionnaireId ?? 0
                };
            }
            else
            {
                var questionCategoryEntity = await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId.Value));
                var config = new MapperConfiguration(c => c.CreateMap<QuestionCategory, QuestionCategoryViewModel>());
                var mapper = new Mapper(config);
                QuestionCategoryViewModel = mapper.Map< QuestionCategoryViewModel>(questionCategoryEntity);
            }
        }

        public async Task OnPostSearchCategories(string nameFragment, QuestionCategoryViewModel questionCategory)
        {
            QuestionCategoryViewModel = questionCategory;

            var config = new MapperConfiguration(c => c.CreateMap<Questionnaire, QuestionnaireViewModel>());
            var mapper = new Mapper(config);
            var questionnaireEntities = await _mediator.Send(new GetQuestionnairesByNameFragmentQuery(nameFragment));
            Questionnaires = mapper.Map<QuestionnaireViewModel[]>(questionnaireEntities);
        }

        public async Task<IActionResult> OnPostCreateQuestionCategory(QuestionCategoryViewModel questionCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<QuestionCategoryViewModel, QuestionCategory>());
                var mapper = new Mapper(config);
                var questionCategoryEntity = mapper.Map<QuestionCategory>(questionCategoryViewModel);
                if (questionCategoryViewModel.Id == 0)
                {
                    await _mediator.Send(new CreateEntityCommand<QuestionCategory>(questionCategoryEntity));
                }
                else
                {
                    await _mediator.Send(new ChangeEntityCommand<QuestionCategory>(questionCategoryEntity));
                }
                await _mediator.Send(new CreateOrChangeEntityCommand<QuestionCategory>(questionCategoryEntity));
                return RedirectToPage("ConcreteQuestionnaire", new { questionnaireId = questionCategoryViewModel.QuestionnaireId });
            }
            ModelState.AddModelError("", "Неправильно введены данные");
            QuestionCategoryViewModel = questionCategoryViewModel;
            Questionnaires = Array.Empty<QuestionnaireViewModel>();
            return Page();
        }
    }
}
