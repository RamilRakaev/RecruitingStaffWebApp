using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class DocumentParseCommand : IRequest<bool>
    {
        public DocumentParseCommand(IFormFile formFile, int jobQuestionnaire, bool parseQuestions = true)
        {
            FormFile = formFile;
            JobQuestionnaire = jobQuestionnaire;
            ParseQuestions = parseQuestions;
        }

        public IFormFile FormFile { get; set; }
        public int JobQuestionnaire { get; set; }
        public bool ParseQuestions { get; set; }
    }
}
