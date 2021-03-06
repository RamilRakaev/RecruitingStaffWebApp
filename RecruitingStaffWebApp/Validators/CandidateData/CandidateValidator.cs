using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.CandidateData;

namespace RecruitingStaff.WebApp.Validators.CandidateData
{
    public class CandidateValidator : AbstractValidator<CandidateViewModel>
    {
        public CandidateValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.DateOfBirth)
                .NotNull();

            RuleFor(c => c.VacancyIds)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.TelephoneNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}
