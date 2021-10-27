using Domain.Model;
using FluentValidation;

namespace CQRS.Queries.Validators
{
    public class GetContenderValidator : AbstractValidator<Contender>
    {
        public GetContenderValidator()
        {
            RuleFor(c => c.Id).NotEqual(0);
        }
    }
}
