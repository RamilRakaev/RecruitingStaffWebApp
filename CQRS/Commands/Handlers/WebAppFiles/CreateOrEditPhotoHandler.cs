using MediatR;
using Microsoft.AspNetCore.Hosting;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.WebAppFiles
{
    public class CreateOrEditPhotoHandler : IRequestHandler<CreateOrEditPhotoCommand, bool>
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IWebHostEnvironment _webHost;

        public CreateOrEditPhotoHandler(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IRepository<Candidate> candidateRepository,
            IWebHostEnvironment webHost)
        {
            _fileRepository = fileRepository;
            _candidateRepository = candidateRepository;
            _webHost = webHost;
        }

        public async Task<bool> Handle(CreateOrEditPhotoCommand request, CancellationToken cancellationToken)
        {
            var file = _fileRepository.GetAll().Where(f => f.CandidateId == request.CandidateId && f.FileType == FileType.Photo).FirstOrDefault();
            var candidate = await _candidateRepository.FindAsync(request.CandidateId);

            var extension = request.FormFile.FileName[request.FormFile.FileName.LastIndexOf('.')..];
            var contentRoot = $"{ _webHost.WebRootPath}\\img";
            var source = $"{candidate.Id}.{candidate.FullName}{extension}";
            
            await request.FormFile.CreateNewFileAsync(contentRoot + "\\" + source);
            if (file == null)
            {
                file = new RecruitingStaffWebAppFile()
                {
                    Source = source,
                    CandidateId = request.CandidateId,
                    FileType = FileType.Photo
                };
                await _fileRepository.AddAsync(file);
            }
            else
            {
                if(file.Source != source)
                {
                    File.Delete(contentRoot + "\\" + file.Source);
                    file.Source = source;
                }
            }
            await _fileRepository.SaveAsync();
            candidate.PhotoId = file.Id;
            await _candidateRepository.SaveAsync();
            return true;
        }
    }
}
