using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.ApplicationUsers;
using FluentValidation;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Validators
{
    public class CheckRoleForUserValidator : AbstractValidator<CheckRoleForUserQuery>
    {
        public CheckRoleForUserValidator()
        {
            RuleFor(q => q.Roles)
                .NotNull()
                .NotEmpty();
        }
    }
}
