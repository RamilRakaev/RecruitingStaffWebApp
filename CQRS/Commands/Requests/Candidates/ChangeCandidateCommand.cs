using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class ChangeCandidateCommand : IRequest<bool>
    {
        public ChangeCandidateCommand(Candidate candidate, IFormFile formFile)
        {
            Candidate = candidate;
            UploadedFile = formFile;
        }

        public Candidate Candidate { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
