using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class CreateOrChangeVacancyHandler : IRequestHandler<CreateOrChangeVacancyCommand, Vacancy>
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public CreateOrChangeVacancyHandler(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<Vacancy> Handle(CreateOrChangeVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository
                .FindAsync(request.Vacancy.Id, cancellationToken) ??
                await _vacancyRepository
                .GetAllAsNoTracking()
                .Where(v => v.Name.Equals(request.Vacancy.Name))
                .FirstOrDefaultAsync(cancellationToken);
            if (vacancy != null)
            {
                request.Vacancy.Id = vacancy.Id;
                await _vacancyRepository.Update(request.Vacancy);
            }
            else
            {
                await _vacancyRepository.AddAsync(request.Vacancy, cancellationToken);
            }
            await _vacancyRepository.SaveAsync(cancellationToken);
            return request.Vacancy;
        }
    }
}
