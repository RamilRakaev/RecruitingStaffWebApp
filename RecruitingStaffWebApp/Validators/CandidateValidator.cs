using FluentValidation;
using RecruitingStaff.WebApp.ViewModels;

namespace RecruitingStaff.WebApp.Validators
{
    public class CandidateValidator : AbstractValidator<CandidateViewModel>
    {
        public CandidateValidator()
        {
            //RuleFor(c => c.Name)
            //    .NotNull();

            //RuleFor(c => c.DateOfBirth)
            //    .NotNull();

            //RuleFor(c => c.TelephoneNumber)
            //    .NotEmpty();
        }
    }
}
