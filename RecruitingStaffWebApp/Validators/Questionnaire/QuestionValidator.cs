using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;

namespace RecruitingStaff.WebApp.Validators.Questionnaire
{
    public class QuestionValidator : AbstractValidator<QuestionViewModel>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty();
            RuleFor(q => q.QuestionCategoryId)
                .NotEqual(0);
        }
    }
}
