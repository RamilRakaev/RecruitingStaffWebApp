using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class GetAnswersByCanidateIdHandler : IRequestHandler<GetAnswersByCanidateIdQuery, Answer[]>
    {
        private readonly IRepository<Answer> _answerRepository;

        public GetAnswersByCanidateIdHandler(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Answer[]> Handle(GetAnswersByCanidateIdQuery request, CancellationToken cancellationToken)
        {
            return await _answerRepository
                .GetAllAsNoTracking()
                .Where(a => a.CandidateId == request.CandidateId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
