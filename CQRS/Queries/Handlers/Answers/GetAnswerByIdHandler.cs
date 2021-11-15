using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class GetAnswerByIdHandler : IRequestHandler<GetAnswerByIdQuery, Answer>
    {
        private readonly IRepository<Answer> _answerRepository;

        public GetAnswerByIdHandler(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Answer> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _answerRepository.FindAsync(request.AnswerId, cancellationToken);
        }
    }
}
