using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class DocumentParseCommand : IRequest<bool>
    {
        public DocumentParseCommand(IFormFile formFile, int jobQuestionnaire)
        {
            FormFile = formFile;
            JobQuestionnaire = jobQuestionnaire;
        }

        public IFormFile FormFile { get; set; }
        public int JobQuestionnaire { get; set; }
    }
}
