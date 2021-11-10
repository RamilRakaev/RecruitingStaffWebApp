using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetQuestionCategoryByIdHandler : IRequestHandler<GetQuestionCategoryByIdQuery, QuestionCategory>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public GetQuestionCategoryByIdHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<QuestionCategory> Handle(GetQuestionCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _questionCategoryRepository.FindAsync(request.QuestionCategoryId);
        }
    }
}
