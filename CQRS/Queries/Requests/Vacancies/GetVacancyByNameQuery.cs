using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies
{
    public class GetVacancyByNameQuery : IRequest<Vacancy>
    {
        public GetVacancyByNameQuery(string vacancyName)
        {
            VacancyName = vacancyName;
        }

        public string VacancyName { get; set; }
    }
}
