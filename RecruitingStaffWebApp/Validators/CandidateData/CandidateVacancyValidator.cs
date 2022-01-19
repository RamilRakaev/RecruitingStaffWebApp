using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.CandidateData;

namespace RecruitingStaff.WebApp.Validators.CandidateData
{
    public class CandidateVacancyValidator : AbstractValidator<CandidateVacancyViewModel>
    {
        public CandidateVacancyValidator()
        {
            RuleFor(cv => cv.FirstEntityId)
                .NotEqual(0);

            RuleFor(cv => cv.SecondEntityId)
                .NotEqual(0);
        }
    }
}
