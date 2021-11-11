using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public class CandidateFileManagement
    {
        protected readonly WebAppOptions _options;
        protected readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private readonly IWebHostEnvironment _webHost;

        public CandidateFileManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost)
        {
            _fileRepository = fileRepository;
            _options = options.Value;
            _webHost = webHost;
        }

        public async Task RewritePhoto(IFormFile formFile, Candidate candidate)
        {
            if (formFile != null)
            {
                if (candidate.Photo != null)
                {
                    await DeleteCandidateDocument(candidate.Photo);
                }
                await SaveDocument(formFile, candidate, null);
            }
        }

        public async Task SaveDocument(IFormFile formFile, Candidate candidate, Questionnaire questionnaire = null)
        {
            var extension = formFile.FileName[formFile.FileName.IndexOf('.')..];
            FileType fileType;
            fileType = FileType.Questionnaire;
            var file = new RecruitingStaffWebAppFile()
            {
                FileType = fileType,
                Source = $"{candidate.Id}.{candidate.FullName} - {questionnaire.Name}{extension}"
            };
            await formFile.CreateNewFileAsync($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.AddAsync(file);
            await _fileRepository.SaveAsync();
        }

        public async Task DeleteQuestionnaireFiles(int questionnaireId)
        {
            var documents = _fileRepository.GetAllAsNoTracking().Where(f => f.QuestionnaireId == questionnaireId);
            if (documents != null)
            {
                foreach (var document in documents.ToArray())
                {
                    await DeleteCandidateDocument(document);
                }
            }
        }

        public async Task DeleteCandidateFiles(int candidateId)
        {
            var files = _fileRepository.GetAllAsNoTracking().Where(f => f.CandidateId == candidateId);
            if (files != null)
            {
                foreach (var file in files.ToArray())
                {
                    if(file.FileType == FileType.Questionnaire)
                    {
                        await DeleteCandidateDocument(file);
                    }
                    else if (file.FileType == FileType.Photo)
                    {
                        await DeleteCandidatePhoto(file);
                    }
                }
            }
        }

        public async Task DeleteCandidateDocument(RecruitingStaffWebAppFile file)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync();
        }

        public async Task DeleteCandidatePhoto(RecruitingStaffWebAppFile file)
        {
            var path = $"{_webHost.WebRootPath}\\img\\{file.Source}";
            File.Delete(path);
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync();
        }
    }
}
