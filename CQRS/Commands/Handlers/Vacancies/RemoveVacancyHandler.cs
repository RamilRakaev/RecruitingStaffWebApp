﻿using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class RemoveVacancyHandler : VacancyCommandHandler, IRequestHandler<RemoveVacancyCommand, bool>
    {
        public RemoveVacancyHandler(
            IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost,
            IRepository<Vacancy> vacancyRepository) : base(answerRepository,
                questionRepository,
                questionCategoryRepository,
                questionnaireRepository,
                fileRepository,
                options,
                vacancyRepository,
                webHost)
        { }

        public async Task<bool> Handle(RemoveVacancyCommand request, CancellationToken cancellationToken)
        {
            await RemoveVacancy(request.VacancyId, cancellationToken);
            return true;
        }
    }
}
