using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetFilesByTypeHandler : IRequestHandler<GetFilesByTypeQuery, RecruitingStaffWebAppFile[]>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;

        public GetFilesByTypeHandler(IRepository<RecruitingStaffWebAppFile> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<RecruitingStaffWebAppFile[]> Handle(GetFilesByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _fileRepository
                .GetAllAsNoTracking()
                .Where(f => f.FileType == request.FileType)
                .ToArrayAsync(cancellationToken);
        }
    }
}
