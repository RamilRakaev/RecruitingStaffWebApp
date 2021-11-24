using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Validators
{
    public class CandidateValidator : AbstractValidator<Candidate>
    {
        public CandidateValidator()
        {
            RuleFor(c => c.FullName)
                .NotNull();

            RuleFor(c => c.Address)
                .NotNull();
        }
    }
}
