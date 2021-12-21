using FluentValidation;
using RecruitingStaff.WebApp.ViewModels;
using RecruitingStaff.WebApp.ViewModels.CandidateData;

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
