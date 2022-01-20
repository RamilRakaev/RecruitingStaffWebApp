using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class GetQuestionCategoriesByQuestionnaireIdHandler : IRequestHandler<GetQuestionCategoriesByQuestionnaireIdQuery, QuestionCategory[]>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public GetQuestionCategoriesByQuestionnaireIdHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public Task<QuestionCategory[]> Handle(GetQuestionCategoriesByQuestionnaireIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_questionCategoryRepository.GetAllExistingEntitiesAsNoTracking().Where(qc => qc.QuestionnaireId == request.QuestionCategoryId).ToArray());
        }
    }
}
