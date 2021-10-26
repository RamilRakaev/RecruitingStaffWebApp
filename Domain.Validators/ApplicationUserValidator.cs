using Domain.Model;
using FluentValidation;

namespace Domain.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(u => u.Email).NotNull().EmailAddress();
        }
    }
}
