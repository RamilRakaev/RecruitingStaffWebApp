using MediatR;
using Microsoft.AspNetCore.Http;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class ChangeCandidateCommand : IRequest<bool>
    {
        public ChangeCandidateCommand(Candidate candidate, IFormFile formFile, int questionnaireId)
        {
            Candidate = candidate;
            UploadedFile = formFile;
            QuestionnaireId = questionnaireId;
        }

        public Candidate Candidate { get; set; }
        public IFormFile UploadedFile { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
