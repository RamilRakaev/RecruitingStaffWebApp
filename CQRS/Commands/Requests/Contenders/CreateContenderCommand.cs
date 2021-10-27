using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CQRS.Commands.Requests.Contenders
{
    public class CreateContenderCommand : IRequest<bool>
    {
        public CreateContenderCommand(Contender contender, IFormFile uploadedFile)
        {
            Contender = contender;
            UploadedFile = uploadedFile;
        }

        public Contender Contender { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
