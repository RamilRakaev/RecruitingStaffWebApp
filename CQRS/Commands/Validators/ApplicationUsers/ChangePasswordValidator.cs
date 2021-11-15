using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.ApplicationUsers
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(10);
        }
    }
}
