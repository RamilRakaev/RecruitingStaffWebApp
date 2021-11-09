using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies
{
    public class GetVacancyQuery : IRequest<Vacancy>
    {
        public GetVacancyQuery(int vacancy)
        {
            VacancyId = vacancy;
        }

        public int VacancyId { get; set; }
    }
}
