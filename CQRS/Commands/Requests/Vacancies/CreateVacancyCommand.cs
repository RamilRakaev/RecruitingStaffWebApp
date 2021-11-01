using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies
{
    public class CreateVacancyCommand : IRequest<bool>
    {
        public CreateVacancyCommand(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }

        public Vacancy Vacancy { get; set; }
    }
}
