using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetQuestionCategoriesHandler : IRequestHandler<GetQuestionCategoriesQuery, QuestionCategory[]>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public GetQuestionCategoriesHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<QuestionCategory[]> Handle(GetQuestionCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _questionCategoryRepository.GetAllAsNoTracking().ToArrayAsync(cancellationToken);
        }
    }
}
