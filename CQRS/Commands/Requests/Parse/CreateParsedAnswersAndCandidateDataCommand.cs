using MediatR;
using RecruitingStaffWebApp.Services.DocParse;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse
{
    public class CreateParsedAnswersAndCandidateDataCommand : IRequest<bool>
    {
        public CreateParsedAnswersAndCandidateDataCommand(ParsedData parsedData)
        {
            ParsedData = parsedData;
        }
        public ParsedData ParsedData { get; set; }
    }
}
