using MediatR;
using RecruitingStaffWebApp.Services.DocParse;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse
{
    public class SaveCompletedQuestionnaireCommand : IRequest<bool>
    {
        public SaveCompletedQuestionnaireCommand(ParsedData parsedData)
        {
            ParsedData = parsedData;
        }
        public ParsedData ParsedData { get; set; }
    }
}
