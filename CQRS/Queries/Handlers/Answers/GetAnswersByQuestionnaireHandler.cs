using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class GetAnswersByQuestionnaireHandler : IRequestHandler<GetAnswersByQuestionCategoryQuery, Answer[]>
    {
        private readonly IRepository<Answer> _answeRepository;
        private readonly IRepository<Question> _questionRepository;

        public GetAnswersByQuestionnaireHandler(IRepository<Answer> answeRepository, IRepository<Question> questionRepository)
        {
            _answeRepository = answeRepository;
            _questionRepository = questionRepository;
        }

        public Task<Answer[]> Handle(GetAnswersByQuestionCategoryQuery request, CancellationToken cancellationToken)
        {
            var questionsIds = _questionRepository
                .GetAllAsNoTracking()
                .Where(q => q.QuestionCategoryId == request.QuestionCategoryId)
                .Select(q => q.Id);
            return _answeRepository
                .GetAllAsNoTracking()
                .Where(a => questionsIds.Contains(a.QuestionId))
                .ToArrayAsync(cancellationToken);
        }
    }
}
