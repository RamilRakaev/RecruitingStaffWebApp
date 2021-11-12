using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.ApplicationUsers
{
    public class CreateOrChangeUserValidator : AbstractValidator<CreateOrChangeUserCommand>
    {
        public CreateOrChangeUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .Matches("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])")
                .WithMessage("Неправильно введён пароль");
        }
    }
}
