using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Parse;

namespace RecruitingStaff.WebApp.Validators.Parse
{
    public class DocumentParseValidator : AbstractValidator<DocumentParseViewModel>
    {
        public DocumentParseValidator()
        {
            RuleFor(dp => dp.FormFile)
                .NotNull();
        }
    }
}
