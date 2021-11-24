using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateCandidateByQuestionnaireCommand : IRequest<bool>
    {
        public CreateCandidateByQuestionnaireCommand(Candidate candidate, int questionnaireId)
        {
            Candidate = candidate;
            QuestionnaireId = questionnaireId;
        }

        public Candidate Candidate { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
