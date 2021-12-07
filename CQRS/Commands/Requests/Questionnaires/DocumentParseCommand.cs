using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class DocumentParseCommand : IRequest<bool>
    {
        public DocumentParseCommand(IFormFile formFile, int jobQuestionnaire, int candidateId = 0, bool parseQuestions = false)
        {
            FormFile = formFile;
            JobQuestionnaire = jobQuestionnaire;
            CandidateId = candidateId;
            ParseQuestions = parseQuestions;
        }

        public IFormFile FormFile { get; set; }
        public int JobQuestionnaire { get; set; }
        public int CandidateId { get; set; }
        public bool ParseQuestions { get; set; }
    }
}
