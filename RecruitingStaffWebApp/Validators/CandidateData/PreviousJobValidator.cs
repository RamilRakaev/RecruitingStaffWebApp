using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.CandidateData;

namespace RecruitingStaff.WebApp.Validators.CandidateData
{
    public class PreviousJobValidator : AbstractValidator<PreviousJobPlacementViewModel>
    {
        public PreviousJobValidator()
        {
            RuleFor(pjp => pjp.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
