using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class RemoveQuestionCommand : IRequest<bool>
    {
        public RemoveQuestionCommand(int vacancyId)
        {
            VacancyId = vacancyId;
        }

        public int VacancyId { get; set; }
    }
}
