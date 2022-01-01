using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Vacancies
{
    //public class GetVacancyHandler : IRequestHandler<GetVacancyQuery, Vacancy>
    //{
    //    private readonly IRepository<Vacancy> _vacancyRepository;

    //    public GetVacancyHandler(IRepository<Vacancy> vacancyRepository)
    //    {
    //        _vacancyRepository = vacancyRepository;
    //    }

    //    public async Task<Vacancy> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
    //    {
    //        return await _vacancyRepository.FindAsync(request.VacancyId, cancellationToken);
    //    }
    //}
}
