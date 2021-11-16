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
            var vacancy = await _vacancyRepository.FindAsync(request.Vacancy.Id, cancellationToken);
            if (vacancy == null)
            {
                vacancy = await _vacancyRepository
                    .GetAllAsNoTracking()
                    .Where(v => v.Name.Equals(request.Vacancy.Name))
                    .FirstOrDefaultAsync(cancellationToken);
                if (vacancy != null)
                {
                    return vacancy;
                }
                await _vacancyRepository.AddAsync(request.Vacancy, cancellationToken);
                await _vacancyRepository.SaveAsync(cancellationToken);
                vacancy = request.Vacancy;
            }
            else
            {
                vacancy.Name = request.Vacancy.Name;
                vacancy.Description = request.Vacancy.Description;
                vacancy.Responsibilities = request.Vacancy.Responsibilities;
                vacancy.Requirements = request.Vacancy.Requirements;
                vacancy.WorkingConditions = request.Vacancy.WorkingConditions;
                await _vacancyRepository.SaveAsync(cancellationToken);
            }
            return vacancy;
        }
    }
}
