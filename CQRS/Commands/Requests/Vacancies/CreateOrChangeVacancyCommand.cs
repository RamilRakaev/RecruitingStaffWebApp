using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class CreateOrChangeVacancyCommand : IRequest<bool>
    {
        public CreateOrChangeVacancyCommand(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }

        public Vacancy Vacancy { get; set; }
    }
}
