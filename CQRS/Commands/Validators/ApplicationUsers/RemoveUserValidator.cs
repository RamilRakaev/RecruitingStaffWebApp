using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.ApplicationUsers
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserValidator()
        {
            RuleFor(b => b.Id)
                .NotNull()
                .NotEqual(0);
        }
    }
}
