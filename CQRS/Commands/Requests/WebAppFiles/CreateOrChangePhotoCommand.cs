using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles
{
    public class CreateOrChangePhotoCommand : IRequest<bool>
    {
        public CreateOrChangePhotoCommand(IFormFile formFile, int candidateId)
        {
            FormFile = formFile;
            CandidateId = candidateId;
        }

        public IFormFile FormFile { get; set; }
        public int CandidateId { get; set; }
    }
}
