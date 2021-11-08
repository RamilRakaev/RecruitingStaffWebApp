using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class RemoveVacancyHandler : VacancyCommandHandler, IRequestHandler<RemoveQuestionCommand, bool>
    {
        public RemoveVacancyHandler(IRepository<Answer> answerRepository, IRepository<Question> questionRepository, IRepository<QuestionCategory> questionCategoryRepository, IRepository<Questionnaire> questionnaireRepository, IRepository<RecruitingStaffWebAppFile> fileRepository, IOptions<WebAppOptions> options, IRepository<Vacancy> vacancyRepository) : base(answerRepository, questionRepository, questionCategoryRepository, questionnaireRepository, fileRepository, options, vacancyRepository)
        { }

        public async Task<bool> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
        {
            await RemoveVacancy(request.VacancyId);
            return true;
        }
    }
}
