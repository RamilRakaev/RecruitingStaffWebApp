using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetSourceOfCandidatePhotoHandler : IRequestHandler<GetSourceOfCandidatePhotoQuery, string>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;

        public GetSourceOfCandidatePhotoHandler(
            IRepository<RecruitingStaffWebAppFile> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> Handle(GetSourceOfCandidatePhotoQuery request, CancellationToken cancellationToken)
        {
            var photo = await _fileRepository
                .GetAllAsNoTracking()
                .Where(f => f.CandidateId == request.CandidateId && f.FileType == FileType.JpgPhoto)
                .FirstOrDefaultAsync(cancellationToken);
            if (photo == null)
            {
                return string.Empty;
            }
            return photo.Name;
        }
    }
}
