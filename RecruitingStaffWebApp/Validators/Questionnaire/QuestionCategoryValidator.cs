using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;

namespace RecruitingStaff.WebApp.Validators.Questionnaire
{
    public class QuestionCategoryValidator : AbstractValidator<QuestionCategoryViewModel>
    {
        public QuestionCategoryValidator()
        {
            RuleFor(qc => qc.Name)
                .NotEmpty();
            RuleFor(qc => qc.QuestionnaireId)
                .NotEqual(0);
        }
    }
}
