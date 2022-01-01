using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class ChangeQuestionnaireCommand : IRequest<bool>
    {
        public ChangeQuestionnaireCommand(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public Questionnaire Questionnaire { get; set; }
    }
}
