using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Domain.Validators
{
    public class QuestionnaireValidator : AbstractValidator<Questionnaire>
    {
        public QuestionnaireValidator()
        {
            RuleFor(q => q.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(q => q.VacancyId)
                .NotEqual(0);
        }
    }
}
