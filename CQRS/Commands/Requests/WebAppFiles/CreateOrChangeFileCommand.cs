using MediatR;
using Microsoft.AspNetCore.Http;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles
{
    public class CreateOrChangeFileCommand : IRequest<RecruitingStaffWebAppFile>
    {
        public CreateOrChangeFileCommand(
            IFormFile formFile,
            string name,
            FileType fileType = FileType.CompletedQuestionnaire,
            int? candidateId = null,
            int? questionnaireId = null,
            int? testTaskId = null)
        {
            FileEntity = new()
            {
                Name = name,
                FileType = fileType,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
                TestTaskId = testTaskId,
            };
            FileEntity.Name = $"{candidateId ?? questionnaireId ?? testTaskId ?? 0}.{FileEntity.Name}";
            FormFile = formFile;
        }

        public CreateOrChangeFileCommand(
            RecruitingStaffWebAppFile fileEntity,
            IFormFile formFile)
        {
            FileEntity = fileEntity;
            FormFile = formFile;
        }

        public RecruitingStaffWebAppFile FileEntity { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
