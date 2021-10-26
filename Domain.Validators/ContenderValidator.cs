using Domain.Model;
using FluentValidation;

namespace Domain.Validators
{
    public class ContenderValidator : AbstractValidator<Contender>
    {
        public ContenderValidator()
        {
            RuleFor(c => c.FullName).NotNull();
            RuleFor(c => c.Address).NotNull();
            RuleFor(c => c.DocumentSource).NotNull().NotEmpty();
        }
    }
}
