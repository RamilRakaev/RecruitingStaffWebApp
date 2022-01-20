using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questions
{
    public class ContainsQuestionByNameHandler : IRequestHandler<ContainsQuestionByNameQuery, bool>
    {
        private readonly IRepository<Question> _questionRepository;

        public ContainsQuestionByNameHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(ContainsQuestionByNameQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository
                .GetAllExistingEntitiesAsNoTracking()
                .Where(q => q.Name.Equals(request.QuestionName))
                .FirstOrDefaultAsync();
            return question != null;
        }
    }
}
