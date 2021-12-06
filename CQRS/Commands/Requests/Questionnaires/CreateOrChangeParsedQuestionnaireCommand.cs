using MediatR;
using RecruitingStaffWebApp.Services.DocParse.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateOrChangeParsedQuestionnaireCommand : IRequest<CommandResult>
    {
        public CreateOrChangeParsedQuestionnaireCommand(QuestionnaireElement questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public QuestionnaireElement Questionnaire { get; set; }
    }
}
