﻿using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class VacancyCommandHandler : QuestionnairesCommandHandlers
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public VacancyCommandHandler(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IRepository<Vacancy> vacancyRepository) 
            : base(answerRepository,
                  questionRepository,
                  questionCategoryRepository,
                  questionnaireRepository,
                  fileRepository,
                  options)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task RemoveVacancy(int vacancyId)
        {
            var vacancy = await _vacancyRepository.FindNoTrackingAsync(vacancyId);
            foreach (var questionnare in vacancy.Questionnaires)
            {
                await RemoveQuestionnaire(questionnare.Id);
            }
            await _vacancyRepository.RemoveAsync(vacancy);
            await _vacancyRepository.SaveAsync();
        }
    }
}