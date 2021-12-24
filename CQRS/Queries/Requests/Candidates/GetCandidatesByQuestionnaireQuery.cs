using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class GetCandidatesByQuestionnaireQuery : IRequest<Candidate[]>
    {
        public GetCandidatesByQuestionnaireQuery(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public int QuestionnaireId { get; set; }
    }
}