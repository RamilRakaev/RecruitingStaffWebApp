using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.FileManagement
{
    public class CandidatePhotosManagement : CandidateFilesManagement
    {
        public CandidatePhotosManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options, IWebHostEnvironment webHost)
            : base(fileRepository, options, webHost)
        {
        }

        public async Task SaveNewPhoto(IFormFile formFile, Candidate candidate, CancellationToken cancellationToken)
        {
            if (formFile != null)
            {
                currentCandidate = candidate;
                if (candidate.PhotoId != null)
                {
                    RecruitingStaffWebAppFile file =
                        await _fileRepository.FindAsync(candidate.PhotoId.Value, cancellationToken);
                    if (file != null)
                    {
                        await DeleteCandidateFile(file, cancellationToken);
                    }
                    else
                    {
                        var extension = formFile.FileName[formFile.FileName.IndexOf('.')..];
                        file = new()
                        {
                            FileType = FileType.Photo,
                            Source = $"{candidate.Id}.{candidate.FullName}{extension}"
                        };
                    }
                    await SaveFile(formFile, file, cancellationToken);
                }
            }
        }

        public async Task DeleteCandidatePhoto(RecruitingStaffWebAppFile file, CancellationToken cancellationToken)
        {
            var path = $"{_webHost.WebRootPath}\\img\\{file.Source}";
            File.Delete(path);
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync(cancellationToken);
        }
    }
}
