using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questions
{
    public class GetQuestionsByCategoryIdHandler : IRequestHandler<GetQuestionsByCategoryIdQuery, Question[]>
    {
        private readonly IRepository<Question> _questionRepository;
        public GetQuestionsByCategoryIdHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Task<Question[]> Handle(GetQuestionsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_questionRepository.GetAllExistingEntitiesAsNoTracking().Where(q => q.QuestionCategoryId == request.QuestionCategoryId).ToArray());
        }
    }
}
