using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies
{
    public class GetVacanciesQuery : IRequest<Vacancy[]>
    {
    }
}
