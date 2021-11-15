using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.Files
{
    public class CreateOrEditPhotoValidator : AbstractValidator<CreateOrEditPhotoCommand>
    {
        public CreateOrEditPhotoValidator()
        {
            RuleFor(c => c.CandidateId)
                .NotEqual(0);
        }
    }
}
