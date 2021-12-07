using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaffWebApp.Services.DocParse;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse
{
    public class CreateOrChangeParsedQuestionnaireCommand : IRequest<Questionnaire>
    {
        public CreateOrChangeParsedQuestionnaireCommand(ParsedData parsedData)
        {
            ParsedData = parsedData;
        }

        public ParsedData ParsedData { get; set; }
    }
}
