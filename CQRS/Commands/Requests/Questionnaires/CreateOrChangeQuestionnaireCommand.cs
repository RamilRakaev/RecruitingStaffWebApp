using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateOrChangeQuestionnaireCommand : IRequest<bool>
    {
        public CreateOrChangeQuestionnaireCommand(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public Questionnaire Questionnaire { get; set; }
    }
}
