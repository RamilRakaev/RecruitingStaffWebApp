using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetQuestionCategoriesByQuestionnaireHandler : IRequestHandler<GetQuestionCategoriesByQuestionnaireQuery, QuestionCategory[]>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public GetQuestionCategoriesByQuestionnaireHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public Task<QuestionCategory[]> Handle(GetQuestionCategoriesByQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_questionCategoryRepository.GetAllAsNoTracking().Where(qc => qc.QuestionnaireId == request.QuestionCategoryId).ToArray());
        }
    }
}
