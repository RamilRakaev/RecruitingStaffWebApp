using FluentValidation;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.WebApp.Validators
{
    public class VacancyValidator : AbstractValidator<Vacancy>
    {
        public VacancyValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
