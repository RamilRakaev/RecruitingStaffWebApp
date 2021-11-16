using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateCandidateCommand : IRequest<Candidate>
    {
        public CreateCandidateCommand(Candidate candidate, int vacancyId, int questionnaireId = 0)
        {
            Candidate = candidate;
            VacancyId = vacancyId;
            QuestionnaireId = questionnaireId;
        }

        public Candidate Candidate { get; set; }
        public int VacancyId { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
