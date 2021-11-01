using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class CreateVacancyHandler : IRequestHandler<CreateVacancyCommand, bool>
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public CreateVacancyHandler(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<bool> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            await _vacancyRepository.AddAsync(request.Vacancy);
            await _vacancyRepository.SaveAsync();
            return true;
        }
    }
}
