using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetCandidatePhotoHandler : IRequestHandler<GetSourceOfCandidatePhotoQuery, string>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;

        public GetCandidatePhotoHandler(
            IRepository<RecruitingStaffWebAppFile> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> Handle(GetSourceOfCandidatePhotoQuery request, CancellationToken cancellationToken)
        {
            var photo = await _fileRepository
                .GetAllAsNoTracking()
                .Where(f => f.CandidateId == request.CandidateId && f.FileType == FileType.Photo)
                .FirstOrDefaultAsync(cancellationToken);
            if (photo == null)
            {
                return string.Empty;
            }
            return photo.Source;
        }
    }
}
