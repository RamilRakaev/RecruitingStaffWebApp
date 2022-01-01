using MediatR;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles
{
    public class GetFilesByTypeQuery : IRequest<RecruitingStaffWebAppFile[]>
    {
        public GetFilesByTypeQuery(FileType fileType)
        {
            FileType = fileType;
        }

        public FileType FileType { get; set; }
    }
}
