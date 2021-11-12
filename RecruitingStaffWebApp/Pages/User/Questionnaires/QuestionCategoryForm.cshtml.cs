using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
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

        public QuestionCategory QuestionCategory { get; set; }
        public Questionnaire[] Questionnaires { get; set; }

        public async Task<IActionResult> OnGet(int? questionCategoryId, int? quesionnaireId)
        {
            Questionnaires = Array.Empty<Questionnaire>();
            if (questionCategoryId == null)
            {
                QuestionCategory = new QuestionCategory() { QuestionnaireId = quesionnaireId ?? 0 };
            }
            else
            {
                QuestionCategory = await _mediator.Send(new GetQuestionCategoryByIdQuery(questionCategoryId.Value));
            }
            return await RightVerification();
        }

        public async Task OnPostSearchCategories(string nameFragment, QuestionCategory questionCategory)
        {
            QuestionCategory = questionCategory;
            Questionnaires = await _mediator.Send(new GetQuestionnairesByNameFragmentQuery(nameFragment));
        }

        public async Task<IActionResult> OnPostCreateQuestionCategory(QuestionCategory questionCategory)
        {
            await _mediator.Send(new CreateOrChangeQuestionCategoryCommand(questionCategory));
            return RedirectToPage("ConcreteQuestionnaire", new { questionnaireId = questionCategory.QuestionnaireId });
        }
    }
}
