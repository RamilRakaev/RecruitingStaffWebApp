using Domain.Model;
using Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies
{
    public class GetVacanciesQuery : IRequest<Vacancy[]>
    {
    }
}
