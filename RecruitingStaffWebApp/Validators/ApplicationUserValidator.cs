using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.ApplicationUser;

namespace RecruitingStaff.Domain.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUserViewModel>
    {
        public ApplicationUserValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
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
