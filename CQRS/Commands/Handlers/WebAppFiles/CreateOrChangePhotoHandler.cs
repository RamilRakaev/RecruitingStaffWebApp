using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class CreateOrChangePhotoHandler : IRequestHandler<CreateOrChangePhotoCommand, bool>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly WebAppOptions _options;

        public CreateOrChangePhotoHandler(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IRepository<Candidate> candidateRepository,
            IOptions<WebAppOptions> options)
        {
            _fileRepository = fileRepository;
            _candidateRepository = candidateRepository;
            _options = options.Value;
        }

        public async Task<bool> Handle(CreateOrChangePhotoCommand request, CancellationToken 
            cancellationToken)
        {
            var file = _fileRepository.GetAllExistingEntities()
                .Where(f => f.CandidateId == request.CandidateId &&
            (f.FileType == FileType.JpgPhoto || f.FileType == FileType.PngPhoto))
                .FirstOrDefault();
            var candidate = await _candidateRepository.FindAsync(request.CandidateId, cancellationToken);

            var extension = request.FormFile.FileName[request.FormFile.FileName.LastIndexOf('.')..];
            var fileName = $"{candidate.Id}.{candidate.Name}{extension}";
            if (file == null)
            {
                file = new RecruitingStaffWebAppFile()
                {
                    Name = fileName,
                    CandidateId = request.CandidateId,
                    FileType = request.FileType
                };
                await _fileRepository.AddAsync(file, cancellationToken);
            }
            else
            {
                File.Delete(Path.Combine(_options.GetSource(request.FileType), file.Name));
                file.Name = fileName;
                file.FileType = request.FileType;
            }
            await request.FormFile.CreateNewFileAsync(Path.Combine(_options.JpgPhotosSource, fileName));
            await _fileRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
