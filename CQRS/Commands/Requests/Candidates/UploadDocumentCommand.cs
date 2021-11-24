using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class UploadDocumentCommand : IRequest<bool>
    {
        public UploadDocumentCommand(IFormFile uploadedFile, int candidateId, int questionnaireId)
        {
            UploadedFile = uploadedFile;
            CandidateId = candidateId;
            QuestionnaireId = questionnaireId;
        }

        public IFormFile UploadedFile { get; set; }

        public int CandidateId { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
