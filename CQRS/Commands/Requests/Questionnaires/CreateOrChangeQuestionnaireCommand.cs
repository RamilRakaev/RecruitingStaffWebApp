using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateOrChangeQuestionnaireCommand : IRequest<Questionnaire>
    {
        public CreateOrChangeQuestionnaireCommand(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public Questionnaire Questionnaire { get; set; }
    }
}
