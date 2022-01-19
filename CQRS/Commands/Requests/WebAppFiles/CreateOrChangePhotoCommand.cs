using MediatR;
using Microsoft.AspNetCore.Http;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles
{
    public class CreateOrChangePhotoCommand : IRequest<bool>
    {
        public CreateOrChangePhotoCommand(IFormFile formFile, int candidateId)
        {
            FormFile = formFile;
            CandidateId = candidateId;
            FileType = formFile.ContentType switch
            {
                "image/jpeg" => FileType.JpgPhoto,
                "image/png" => FileType.PngPhoto,
                _ => FileType.JpgPhoto,
            };
        }

        public IFormFile FormFile { get; set; }
        public int CandidateId { get; set; }
        public FileType FileType { get; set; }
    }
}
