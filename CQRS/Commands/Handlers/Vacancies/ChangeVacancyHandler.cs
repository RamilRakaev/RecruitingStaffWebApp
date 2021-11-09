using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class ChangeVacancyHandler : IRequestHandler<ChangeVacancyCommand, bool>
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public ChangeVacancyHandler(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<bool> Handle(ChangeVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.FindAsync(request.Vacancy.Id);
            vacancy.Name = request.Vacancy.Name;
            vacancy.Description = request.Vacancy.Description;
            vacancy.Responsibilities = request.Vacancy.Responsibilities;
            vacancy.Requirements = request.Vacancy.Requirements;
            vacancy.WorkingConditions = request.Vacancy.WorkingConditions;
            await _vacancyRepository.SaveAsync();
            return true;
        }
    }
}
