using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.QuestionCategories
{
    public class ContainsQuestionCategoryByNameHandler : IRequestHandler<ContainsQuestionCategoryByNameQuery, bool>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public ContainsQuestionCategoryByNameHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<bool> Handle(ContainsQuestionCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _questionCategoryRepository
                .GetAllAsNoTracking()
                .Where(qc => qc.Name.Equals(request.QuestionCategoryName))
                .FirstOrDefaultAsync();
            return vacancy != null;
        }
    }
}
