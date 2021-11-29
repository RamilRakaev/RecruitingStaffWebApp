using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetConcreteQuestionnaireHandler : IRequestHandler<GetConcreteQuestionnaireQuery, Dictionary<QuestionCategory, Question[]>>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;
        private readonly IRepository<Question> _questionRepository;

        public GetConcreteQuestionnaireHandler(IRepository<QuestionCategory> questionCategoryRepository, IRepository<Question> questionRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
            _questionRepository = questionRepository;
        }

        public async Task<Dictionary<QuestionCategory, Question[]>> Handle(GetConcreteQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            var questionCategories =
                _questionCategoryRepository
                .GetAllAsNoTracking()
                .Where(qc => qc.QuestionnaireId == request.QuestionnaireId);
            var categoriesIds = questionCategories.Select(qc => qc.Id);
            var questions = _questionRepository.GetAllAsNoTracking().Where(q => categoriesIds.Contains(q.QuestionCategoryId));
            Dictionary<QuestionCategory, Question[]> questionnaire = new();
            foreach(var questionCategory in questionCategories)
            {
                questionnaire.Add(questionCategory,
                    await questions
                    .Where(q => q.QuestionCategoryId == questionCategory.Id)
                    .ToArrayAsync(cancellationToken));
            }
            return questionnaire;
        }
    }
}
