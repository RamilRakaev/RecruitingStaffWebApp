﻿using Microsoft.AspNetCore.Http;
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

        public CandidateFileManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options)
        {
            _fileRepository = fileRepository;
            _options = options.Value;
        }

        public async Task RewritePhoto(IFormFile formFile, Candidate candidate)
        {
            if (formFile != null)
            {
                if (candidate.Photo != null)
                {
                    await DeleteCandidateFile(candidate.Photo);
                }
                await SaveFile(formFile, candidate, null);
            }
        }

        public async Task SaveFile(IFormFile formFile, Candidate candidate, Questionnaire questionnaire)
        {
            var extension = formFile.FileName[formFile.FileName.IndexOf('.')..];
            FileType fileType;
            if(questionnaire != null)
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

        public async Task DeleteQuestionnaireFiles(int questionnaireId)
        {
            var documents = _fileRepository.GetAllAsNoTracking().Where(f => f.QuestionnaireId == questionnaireId);
            if (documents != null)
            {
                foreach (var document in documents.ToArray())
                {
                    await DeleteCandidateFile(document);
                }
            }
        }

        public async Task DeleteCandidateFiles(int candidateId)
        {
            var documents = _fileRepository.GetAllAsNoTracking().Where(f => f.CandidateId == candidateId);
            if (documents != null)
            {
                foreach (var document in documents.ToArray())
                {
                    await DeleteCandidateFile(document);
                }
            }
        }

        public async Task DeleteCandidateFile(RecruitingStaffWebAppFile file)
        {
            File.Delete($"{_options.DocumentsSource}\\{file.Source}");
            await _fileRepository.RemoveAsync(file);
            await _fileRepository.SaveAsync();
        }

        public async Task DeleteCandidateFile(int fileId)
        {
            var file = await _fileRepository.FindAsync(fileId);
            await DeleteCandidateFile(file);
        }
    }
}
