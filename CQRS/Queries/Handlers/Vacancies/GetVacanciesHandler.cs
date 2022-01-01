using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using RecruitingStaff.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Vacancies
{
    //public class GetVacanciesHandler : IRequestHandler<GetVacanciesQuery, Vacancy[]>
    //{
    //    private readonly IRepository<Vacancy> _vacancyRepository;

    //    public GetVacanciesHandler(IRepository<Vacancy> vacancyRepository)
    //    {
    //        _vacancyRepository = vacancyRepository;
    //    }

    //    public Task<Vacancy[]> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(_vacancyRepository.GetAllAsNoTracking().ToArray());
    //    }
    //}
}
