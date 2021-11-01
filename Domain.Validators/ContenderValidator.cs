using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Domain.Validators
{
    public class CandidateValidator : AbstractValidator<Candidate>
    {
        public CandidateValidator()
        {
            RuleFor(c => c.FullName).NotNull();
            RuleFor(c => c.Address).NotNull();
            RuleFor(c => c.DocumentSource).NotNull().NotEmpty();
        }
    }
}
