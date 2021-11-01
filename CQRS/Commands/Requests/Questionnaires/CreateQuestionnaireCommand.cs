using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateQuestionnaireCommand : IRequest<bool>
    {
        public CreateQuestionnaireCommand(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public Questionnaire Questionnaire { get; set; }
    }
}
