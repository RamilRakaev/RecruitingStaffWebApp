using FluentValidation;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Validators.Candidates
{
    public class CreateCandidateValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateValidator()
        {
            RuleFor(c => c.VacancyId)
                .NotEqual(0);

            RuleFor(c => c.Candidate)
                .NotNull();
        }
    }
}
