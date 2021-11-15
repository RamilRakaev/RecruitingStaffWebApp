using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public class CandidateFileManagement
    {
        protected readonly WebAppOptions _options;
        protected readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private readonly IWebHostEnvironment _webHost;
        private Candidate currentCandidate;

        public CandidateFileManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost)
        {
            _fileRepository = fileRepository;
            _options = options.Value;
            _webHost = webHost;
        }

        public async Task RewritePhoto(IFormFile formFile, Candidate candidate, CancellationToken cancellationToken)
        {
            if (formFile != null)
            {
                currentCandidate = candidate;
                if (candidate.PhotoId != null)
                {
                    var file = await _fileRepository.FindAsync(candidate.PhotoId.Value, cancellationToken);
                    if (candidate.Photo != null)
                    {
                        await DeleteCandidateDocument(file, cancellationToken);
                    }
                    await SaveDocument(formFile, $"{candidate.Id}.{candidate.FullName} - фото", cancellationToken);
                }
            }
        }

        public async Task SaveDocument(IFormFile formFile, string fileNameWithoutExtension, CancellationToken cancellationToken)
        {
            var extension = formFile.FileName[formFile.FileName.IndexOf('.')..];
            FileType fileType;
            fileType = FileType.Questionnaire;
            var file = new RecruitingStaffWebAppFile()
            {
                FileType = fileType,
                Source = $"{fileNameWithoutExtension}{extension}"
            };
            await formFile.CreateNewFileAsync($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.AddAsync(file, cancellationToken);
            await _fileRepository.SaveAsync(cancellationToken);
        }

        public async Task DeleteQuestionnaireFiles(int questionnaireId, CancellationToken cancellationToken)
        {
            var documents = _fileRepository.GetAllAsNoTracking().Where(f => f.QuestionnaireId == questionnaireId);
            if (documents != null)
            {
                foreach (var document in documents.ToArray())
                {
                    await DeleteCandidateDocument(document, cancellationToken);
                }
            }
        }

        public async Task DeleteCandidateFiles(int candidateId, CancellationToken cancellationToken)
        {
            var files = _fileRepository.GetAllAsNoTracking().Where(f => f.CandidateId == candidateId);
            if (files != null)
            {
                foreach (var file in files.ToArray())
                {
                    if (file.FileType == FileType.Questionnaire)
                    {
                        await DeleteCandidateDocument(file, cancellationToken);
                    }
                    else if (file.FileType == FileType.Photo)
                    {
                        await DeleteCandidatePhoto(file, cancellationToken);
                    }
                }
            }
        }

        public async Task DeleteCandidateDocument(RecruitingStaffWebAppFile file, CancellationToken cancellationToken)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync(cancellationToken);
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
