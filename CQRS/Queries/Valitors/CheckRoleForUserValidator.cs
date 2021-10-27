﻿using CQRS.Queries.Requests.ApplicationUsers;
using FluentValidation;

namespace CQRS.Queries.Valitors
{
    public class CheckRoleForUserValidator : AbstractValidator<CheckRoleForUserQuery>
    {
        public CheckRoleForUserValidator()
        {
            RuleFor(q => q.Roles).NotNull().NotEmpty();
        }
    }
}
