using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class RemoveVacancyHandler : CandidateFilesRewriter, IRequestHandler<RemoveVacancyCommand, bool>
    {
        private readonly IRepository<Vacancy> _vacancyRepository;
        private readonly IRepository<Questionnaire> _questionareRepository;

        public RemoveVacancyHandler(
            IRepository<Vacancy> vacancyRepository,
            IRepository<Questionnaire> questionareRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) : base(fileRepository, options)
        {
            _vacancyRepository = vacancyRepository;
            _questionareRepository = questionareRepository;
        }

        public async Task<bool> Handle(RemoveVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.FindAsync(request.VacancyId);
            await _vacancyRepository.RemoveAsync(vacancy);
            foreach(var questionare in vacancy.Questionnaires)
            {
                await DeleteFile(questionare.DocumentFile, cancellationToken);
                await _questionareRepository.RemoveAsync(questionare);
            }
            await _vacancyRepository.SaveAsync();
            return true;
        }
    }
}
