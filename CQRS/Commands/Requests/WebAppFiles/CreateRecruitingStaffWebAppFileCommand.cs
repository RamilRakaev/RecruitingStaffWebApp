using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles
{
    public class CreateRecruitingStaffWebAppFileCommand : IRequest<bool>
    {
        public CreateRecruitingStaffWebAppFileCommand(RecruitingStaffWebAppFile file)
        {
            File = file;
        }

        public RecruitingStaffWebAppFile File { get; set; }
    }
}
