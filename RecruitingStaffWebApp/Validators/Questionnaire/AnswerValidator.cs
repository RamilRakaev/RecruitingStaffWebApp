using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Questionnaire;

namespace RecruitingStaff.WebApp.Validators.Questionnaire
{
    public class AnswerValidator : AbstractValidator<AnswerViewModel>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty();
            RuleFor(a => a.CandidateId)
                .NotEqual(0);
        }
    }
}
