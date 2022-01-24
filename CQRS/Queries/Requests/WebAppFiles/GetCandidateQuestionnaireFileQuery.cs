using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles
{
    public class GetCandidateQuestionnaireFileQuery : IRequest<RecruitingStaffWebAppFile>
    {
        public GetCandidateQuestionnaireFileQuery(int candidateId, int questionnaireId)
        {
            CandidateId = candidateId;
            QuestionnaireId = questionnaireId;
        }

        public int CandidateId { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
