using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class RemoveVacancyCommand : IRequest<bool>
    {
        public RemoveVacancyCommand(int vacancyId)
        {
            VacancyId = vacancyId;
        }

        public int VacancyId { get; set; }
    }
}
