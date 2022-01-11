using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles
{
    public class GetFileSourceQuery : IRequest<string>
    {
        public GetFileSourceQuery(FileType fileType)
        {
            FileType = fileType;
        }

        public GetFileSourceQuery(int fileType)
        {
            FileType = (FileType)fileType;
        }

        public FileType FileType { get; set; }
    }
}
