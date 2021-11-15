using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questions
{
    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionByIdQuery, Question>
    {
        private readonly IRepository<Question> _questionRepository;

        public GetQuestionByIdHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _questionRepository.FindAsync(request.QuestionId, cancellationToken);
        }
    }
}
