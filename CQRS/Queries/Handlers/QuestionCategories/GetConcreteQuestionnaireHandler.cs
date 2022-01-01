using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
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
                await _questionCategoryRepository
                .GetAllAsNoTracking()
                .Where(qc => qc.QuestionnaireId == request.QuestionnaireId).ToArrayAsync(cancellationToken);
            var categoriesIds = questionCategories.Select(qc => qc.Id);
            var questions = await _questionRepository.GetAllAsNoTracking().Where(q => categoriesIds.Contains(q.QuestionCategoryId)).ToArrayAsync(cancellationToken);
            Dictionary<QuestionCategory, Question[]> questionnaire = new();
            foreach(var questionCategory in questionCategories)
            {
                questionnaire.Add(questionCategory,
                    questions
                    .Where(q => q.QuestionCategoryId == questionCategory.Id).ToArray());
            }
            return questionnaire;
        }
    }
}
