using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.FileManagement
{
    public class CandidateDocumentsManagement : CandidateFilesManagement
    {
        public CandidateDocumentsManagement(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost)
            : base(
                  fileRepository,
                  options,
                  webHost)
        {
        }

        public async Task SaveDocument(
            IFormFile formFile,
            Candidate candidate,
            string questionnaireName,
            CancellationToken cancellationToken)
        {
            RecruitingStaffWebAppFile file = new()
            {
                FileType = FileType.Questionnaire,
                CandidateId = candidate.Id,
                Source = $"{candidate.Id}.{candidate.FullName} - {questionnaireName}.docx"
            };
            await SaveFile(formFile, file, cancellationToken);
        }
    }
}
