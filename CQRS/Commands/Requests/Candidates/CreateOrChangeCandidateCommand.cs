using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
{
    public class CreateOrChangeCandidateCommand : IRequest<Candidate>
    {
        public CreateOrChangeCandidateCommand(Candidate candidate, int vacancyId, int questionnaireId)
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
