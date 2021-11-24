using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public class CandidateFilesManagement
    {
        private protected readonly WebAppOptions _options;
        private protected readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private protected readonly IWebHostEnvironment _webHost;
        private protected Candidate currentCandidate;

        public CandidateFilesManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost)
        {
            _fileRepository = fileRepository;
            _options = options.Value;
            _webHost = webHost;
        }

        public async Task SaveFile(IFormFile formFile, RecruitingStaffWebAppFile file, CancellationToken cancellationToken)
        {
            await formFile.CreateNewFileAsync($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.AddAsync(file, cancellationToken);
            await _fileRepository.SaveAsync(cancellationToken);
        }

        public async Task DeleteCandidateFiles(int candidateId, CancellationToken cancellationToken)
        {
            var files = _fileRepository.GetAllAsNoTracking().Where(f => f.CandidateId == candidateId);
            if (files != null)
            {
                foreach (var file in files.ToArray())
                {
                    await DeleteCandidateFile(file, cancellationToken);
                }
            }
        }

        public async Task DeleteCandidateFile(RecruitingStaffWebAppFile file, CancellationToken cancellationToken)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync(cancellationToken);
        }

        public async Task DeleteQuestionnaireFiles(int questionnaireId, CancellationToken cancellationToken)
        {
            var documents = _fileRepository.GetAllAsNoTracking().Where(f => f.QuestionnaireId == questionnaireId);
            if (documents != null)
            {
                foreach (var document in documents.ToArray())
                {
                    await DeleteCandidateFile(document, cancellationToken);
                }
            }
        }
    }
}
