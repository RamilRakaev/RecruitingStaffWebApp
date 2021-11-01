using Domain.Model;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class CreateVacancyCommand : IRequest<Vacancy>
    {
        public CreateVacancyCommand(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }

        public Vacancy Vacancy { get; set; }
    }
}
