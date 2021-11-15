using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Validators
{
    public class GetCandidateValidator : AbstractValidator<Candidate>
    {
        public GetCandidateValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }
    }
}
