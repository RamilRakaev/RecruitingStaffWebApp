using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.CandidateQuestionnaires
{
    public class GetAvailableQuestionnairesQuery : IRequest<Questionnaire[]>
    {
        public GetAvailableQuestionnairesQuery(int candidateId)
        {
            CandidateId = candidateId;
        }

        public int CandidateId { get; set; }
    }
}
