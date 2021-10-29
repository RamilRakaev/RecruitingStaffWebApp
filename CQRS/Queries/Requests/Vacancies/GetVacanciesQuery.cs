using Domain.Model;
using MediatR;

namespace CQRS.Queries.Requests.Vacancies
{
    public class GetVacanciesQuery : IRequest<Vacancy[]>
    {
    }
}
