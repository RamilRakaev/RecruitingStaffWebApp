using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class ChangeVacancyCommand : IRequest<bool>
    {
        public ChangeVacancyCommand(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }

        public Vacancy Vacancy { get; set; }
    }
}
