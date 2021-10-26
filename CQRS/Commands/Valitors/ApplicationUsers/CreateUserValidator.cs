using FluentValidation;
using Infrastructure.CQRS.Commands.Requests.ApplicationUsers;

namespace Infrastructure.CQRS.Commands.Validators.ApplicationUsers.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$")
                .WithMessage("Неправильно введён пароль");

            RuleFor(u => u.RoleId)
                .NotEqual(0);
        }
    }
}
