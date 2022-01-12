using FluentValidation;
using RecruitingStaff.WebApp.ViewModels.Files;

namespace RecruitingStaff.WebApp.Validators
{
    public class FileValidator : AbstractValidator<FileViewModel>
    {
        public FileValidator()
        {
            RuleFor(f => f.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(f => f)
                .Must(f => f.QuestionnaireId != null || f.TestTaskId != null || f.CandidateId != null);

            RuleFor(f => f.FileType)
                .NotEqual(0);
        }
    }
}
