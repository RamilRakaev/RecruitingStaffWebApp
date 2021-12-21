using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
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
                QuestionCategoryViewModel = GetViewModel<QuestionCategory, QuestionCategoryViewModel>(
                    await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId.Value)));
            }
        }

        public async Task OnPostSearchCategories(string nameFragment, QuestionCategoryViewModel questionCategory)
        {
            QuestionCategoryViewModel = questionCategory;

            Questionnaires = GetViewModels<Questionnaire, QuestionnaireViewModel>(
                await _mediator.Send(new GetQuestionnairesByNameFragmentQuery(nameFragment)));
        }

        public async Task<IActionResult> OnPostCreateQuestionCategory(QuestionCategoryViewModel questionCategoryViewModel)
        {
            var questionCategoryEntity =
                GetEntity<QuestionCategory, QuestionCategoryViewModel>(
                questionCategoryViewModel);
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
    }
}
