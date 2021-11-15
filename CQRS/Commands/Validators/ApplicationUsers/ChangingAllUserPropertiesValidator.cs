using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.ApplicationUsers
{
    public class ChangingAllUserPropertiesValidator : AbstractValidator<ChangingAllPropertiesCommand>
    {
        public ChangingAllUserPropertiesValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);

            RuleFor(c => c.RoleId)
                .NotEmpty();

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
