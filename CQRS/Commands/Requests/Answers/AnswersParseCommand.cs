using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class AnswersParseCommand : IRequest<bool>
    {
        public AnswersParseCommand(IFormFile formFile, int jobQuestionnaire)
        {
            FormFile = formFile;
            JobQuestionnaire = jobQuestionnaire;
        }

        public IFormFile FormFile { get; set; }
        public int JobQuestionnaire { get; set; }
    }
}
