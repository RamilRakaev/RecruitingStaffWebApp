using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Vacancies;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Vacancies
{
    public class GetVacancyByNameHandler : IRequestHandler<GetVacancyByNameQuery, Vacancy>
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public GetVacancyByNameHandler(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<Vacancy> Handle(GetVacancyByNameQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyRepository
               .GetAllAsNoTracking()
               .Where(v => v.Name.Equals(request.VacancyName))
               .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
