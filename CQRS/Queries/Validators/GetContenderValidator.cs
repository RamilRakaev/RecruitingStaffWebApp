using Domain.Model;
using FluentValidation;

namespace CQRS.Queries.Validators
{
    public class GetCandidateValidator : AbstractValidator<Candidate>
    {
        public GetCandidateValidator()
        {
            RuleFor(c => c.Id).NotEqual(0);
        }
    }
}
