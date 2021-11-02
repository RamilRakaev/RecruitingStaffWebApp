using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public class CandidateFilesRewriter
    {
        protected readonly WebAppOptions _options;
        protected readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;

        public CandidateFilesRewriter(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options)
        {
            _fileRepository = fileRepository;
            _options = options.Value;
        }

        protected async Task RewritePhoto(IFormFile formFile, Candidate candidate, CancellationToken cancellationToken)
        {
            if (formFile != null)
            {
                if (candidate.Photo != null)
                {
                    await DeleteFile(candidate.Photo, cancellationToken);
                }
                await WritePhoto(formFile, candidate, cancellationToken);
            }
        }

        protected async Task WritePhoto(IFormFile formFile, Candidate candidate, CancellationToken cancellationToken)
        {
                var file = new RecruitingStaffWebAppFile()
                {
                    FileType = FileType.Photo,
                    Source = $"{candidate.Id}.{candidate.FullName}.docx"
                };
                await formFile.CreateNewFileAsync($"{_options.DocumentsSource}\\{file.Source}", cancellationToken);
                await _fileRepository.AddAsync(file);
                await _fileRepository.SaveAsync();
        }

        protected async Task DeleteFile(RecruitingStaffWebAppFile file, CancellationToken cancellationToken)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync();
        }
    }
}
