using Domain.Model;
using Domain.Model.CandidateQuestionnaire;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
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
