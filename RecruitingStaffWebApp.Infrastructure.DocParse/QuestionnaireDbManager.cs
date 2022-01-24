using MediatR;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaffWebApp.Services.DocParse;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireDbManager
    {
        private readonly IMediator _mediator;

        public QuestionnaireDbManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SaveParsedDataInDb(ParsedData parsedData, bool isCompletedQuestionnaire = false)
        {
            if (isCompletedQuestionnaire)
            {
                await _mediator.Send(new SaveCompletedQuestionnaireCommand(parsedData));
            }
            else
            {
                await _mediator.Send(new SaveQuestionnaireExampleCommand(parsedData));
            }
        }
    }
}
