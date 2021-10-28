using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CQRS.Commands.Requests.Candidates
{
    public class CreateCandidateCommand : IRequest<bool>
    {
        public CreateCandidateCommand(Candidate candidate, IFormFile uploadedFile)
        {
            Candidate = candidate;
            UploadedFile = uploadedFile;
        }

        public Candidate Candidate { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
