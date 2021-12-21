using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetQuestionCategoriesWithQuestionsHandler : IRequestHandler<GetQuestionCategoriesWithQuestionsQuery, QuestionCategory[]>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;
        private readonly IRepository<Question> _questionRepository;

        public GetQuestionCategoriesWithQuestionsHandler(
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Question> questionRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
            _questionRepository = questionRepository;
        }

        public Task<QuestionCategory[]> Handle(GetQuestionCategoriesWithQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questionCategories =
                _questionCategoryRepository
                .GetAllAsNoTracking()
                .Where(qc => qc.QuestionnaireId == request.QuestionnaireId)
                .ToArray();
            var ids = questionCategories.Select(qc => qc.Id);
            var questions = _questionRepository
                .GetAllAsNoTracking()
                .Where(q => ids.Contains(q.QuestionCategoryId));
            foreach (var questionCategory in questionCategories)
            {
                questionCategory.Questions = questions
                    .Where(q => q.QuestionCategoryId == questionCategory.Id)
                    .ToList();
            }
            return Task.FromResult(questionCategories);
        }
    }
}
