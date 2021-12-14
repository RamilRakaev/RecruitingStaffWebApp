using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Domain.Validators
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
