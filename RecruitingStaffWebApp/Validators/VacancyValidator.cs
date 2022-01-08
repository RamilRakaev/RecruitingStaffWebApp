using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.CandidateData;

namespace RecruitingStaff.WebApp.Validators
{
    public class VacancyValidator : AbstractValidator<VacancyViewModel>
    {
        public VacancyValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
