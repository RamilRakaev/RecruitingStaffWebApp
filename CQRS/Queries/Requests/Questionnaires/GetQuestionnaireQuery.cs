using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires
{
    public class GetQuestionnaireQuery : IRequest<Questionnaire>
    {
        public GetQuestionnaireQuery(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public int QuestionnaireId { get; set; }
    }
}
