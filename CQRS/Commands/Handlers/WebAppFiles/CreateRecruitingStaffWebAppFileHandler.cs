using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class CreateRecruitingStaffWebAppFileHandler : IRequestHandler<CreateRecruitingStaffWebAppFileCommand, bool>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;

        public CreateRecruitingStaffWebAppFileHandler(IRepository<RecruitingStaffWebAppFile> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(CreateRecruitingStaffWebAppFileCommand request, CancellationToken cancellationToken)
        {
            await _fileRepository.AddAsync(request.File, cancellationToken);
            await _fileRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
