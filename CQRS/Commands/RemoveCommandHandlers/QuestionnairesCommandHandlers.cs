﻿using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers
{
    public class QuestionnairesCommandHandlers : QuestionCategoryRemoveHandler
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;
        private readonly CandidateFileManagement rewriter;

        public QuestionnairesCommandHandlers(IRepository<Answer> answerRepository,
            IRepository<Question> questionRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) 
            : base(answerRepository, questionRepository, questionCategoryRepository)
        {
            _questionnaireRepository = questionnaireRepository;
            rewriter = new CandidateFileManagement(fileRepository, options);
        }

        public async Task RemoveQuestionnaire(int questionnireId)
        {
            var questionnaire = await _questionnaireRepository.FindNoTrackingAsync(questionnireId);
            foreach(var questionCategory in questionnaire.QuestionCategories)
            {
                await RemoveQuestionCategory(questionCategory.Id);
            }
            foreach(var file in questionnaire.DocumentFiles)
            {
                await rewriter.DeleteFile(file);
            }
            await _questionnaireRepository.RemoveAsync(questionnaire);
            await _questionnaireRepository.SaveAsync();
        }
    }
}
