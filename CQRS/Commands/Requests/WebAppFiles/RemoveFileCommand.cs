using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles
{
    public class RemoveFileCommand : IRequest<RecruitingStaffWebAppFile>
    {
        public RemoveFileCommand(int fileId)
        {
            FileId = fileId;
        }

        public int FileId { get; set; }
    }
}
