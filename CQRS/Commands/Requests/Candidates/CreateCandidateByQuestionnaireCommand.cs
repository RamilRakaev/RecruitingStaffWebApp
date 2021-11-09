using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

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
