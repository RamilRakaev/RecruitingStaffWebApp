using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class AnswersOnQuestionHandler : IRequestHandler<AnswersOnQuestionQuery, Answer[]>
    {
        private readonly IRepository<Answer> _answerRepository;

        public AnswersOnQuestionHandler(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public Task<Answer[]> Handle(AnswersOnQuestionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_answerRepository.GetAllExistingEntitiesAsNoTracking().Where(a => a.QuestionId == request.QuestionId).ToArray());
        }
    }
}
