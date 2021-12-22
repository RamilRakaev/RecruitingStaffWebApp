using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;

namespace RecruitingStaff.WebApp.Validators.Questionnaire
{
    public class QuestionnaireValidator : AbstractValidator<QuestionnaireViewModel>
    {
        public QuestionnaireValidator()
        {
            RuleFor(q => q.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(q => q.VacancyId)
                .NotEqual(0);
        }
    }
}
