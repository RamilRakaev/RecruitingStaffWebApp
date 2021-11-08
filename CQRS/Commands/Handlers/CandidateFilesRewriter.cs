using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.IO;
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

        public async Task RewritePhoto(IFormFile formFile, Candidate candidate, Questionnaire questionnaire)
        {
            if (formFile != null)
            {
                if (candidate.Photo != null)
                {
                    await DeleteFile(candidate.Photo);
                }
                await SaveFile(formFile, candidate, questionnaire);
            }
        }

        public async Task SaveFile(IFormFile formFile, Candidate candidate, Questionnaire questionnaire)
        {
            var extension = formFile.FileName[formFile.FileName.IndexOf('.')..];
            FileType fileType;
            if(extension == ".doc" || extension == ".docx")
            {
                fileType = FileType.Questionnaire;
            }
            else
            {
                fileType = FileType.Photo;
            }
            var file = new RecruitingStaffWebAppFile()
            {
                FileType = fileType,
                Source = $"{candidate.Id}.{candidate.FullName} - {questionnaire.Name}{extension}"
            };
            await formFile.CreateNewFileAsync($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.AddAsync(file);
            await _fileRepository.SaveAsync();
        }

        public async Task DeleteFile(RecruitingStaffWebAppFile file)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync();
        }
    }
}
