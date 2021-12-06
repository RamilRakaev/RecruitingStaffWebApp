using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questions
{
    public class CreateOrChangeQuestionHandler : IRequestHandler<CreateOrChangeQuestionCommand, bool>
    {
        private readonly IRepository<Question> _questionRepository;

        public CreateOrChangeQuestionHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(CreateOrChangeQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository
                .FindAsync(request.Question.Id, cancellationToken) ??
                await _questionRepository
                    .GetAllAsNoTracking()
                    .Where(q => q.Name.Equals(request.Question.Name)
                && q.QuestionCategoryId == request.Question.QuestionCategoryId)
                    .FirstOrDefaultAsync(cancellationToken);
            if (question == null)
            {
                request.Question.QuestionCategory = null;
                await _questionRepository.AddAsync(request.Question, cancellationToken);
            }
            else
            {
                request.Question.Id = question.Id;
                request.Question.QuestionCategoryId = question.QuestionCategoryId;
                await _questionRepository.Update(request.Question);
            }
            await _questionRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
