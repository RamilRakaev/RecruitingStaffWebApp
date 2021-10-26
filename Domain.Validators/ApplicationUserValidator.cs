using Domain.Model.UserIdentity;
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
