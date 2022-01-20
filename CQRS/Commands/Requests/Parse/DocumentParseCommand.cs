using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse
{
    public class DocumentParseCommand : IRequest<bool>
    {
        public DocumentParseCommand(IFormFile formFile, bool parseQuestions = true, int candidateId = 0, int questionnaireId = 0)
        {
            FormFile = formFile;
            ParseQuestions = parseQuestions;
            CandidateId = candidateId;
            QuestionnaireId = questionnaireId;
        }

        public IFormFile FormFile { get; set; }
        public int CandidateId { get; set; }
        public int QuestionnaireId { get; set; }
        public bool ParseQuestions { get; set; }
    }
}
