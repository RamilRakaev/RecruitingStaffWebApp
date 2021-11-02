using FluentValidation;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Domain.Validators
{
    public class QuestionnaireValidator : AbstractValidator<Questionnaire>
    {
        public QuestionnaireValidator()
        {
            RuleFor(q => q.Name).NotNull().NotEmpty();
            RuleFor(q => q.CandidateId).NotEqual(0);
            RuleFor(q => q.VacancyId).NotEqual(0);
            RuleFor(q => q.DocumentFileId).NotEqual(0);
        }
    }
}
