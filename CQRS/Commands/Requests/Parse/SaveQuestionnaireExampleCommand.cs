using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaffWebApp.Services.DocParse;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse
{
    public class SaveQuestionnaireExampleCommand : IRequest<Questionnaire>
    {
        public SaveQuestionnaireExampleCommand(ParsedData parsedData)
        {
            ParsedData = parsedData;
        }

        public ParsedData ParsedData { get; set; }
    }
}
