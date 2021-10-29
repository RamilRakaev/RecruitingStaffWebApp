using Domain.Model;
using FluentValidation;

namespace Domain.Validators
{
    public class VacancyValidator : AbstractValidator<Vacancy>
    {
        public VacancyValidator()
        {
            RuleFor(v => v.Name).NotNull().NotEmpty();
        }
    }
}
