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

        public async Task SaveParsedData(ParsedData parsedData, bool parseQuestions = false)
        {
            if (parseQuestions)
            {
                await _mediator.Send(new CreateOrChangeParsedQuestionnaireCommand(parsedData));
            }
            else
            {
                await _mediator.Send(new CreateParsedAnswersAndCandidateDataCommand(parsedData));
            }
        }

        
    }
}
