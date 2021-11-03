using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class DocumentParseCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentName">Example: 1.Александров Александр Александрович.docx</param>
        public DocumentParseCommand(IFormFile formFile)
        {
            FormFile = formFile;
        }

        public IFormFile FormFile { get; set; }
    }
}
