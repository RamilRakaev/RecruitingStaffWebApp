using MediatR;
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
            var questionnaire = await _questionRepository.FindAsync(request.Question.Id);
            if (questionnaire == null)
            {
                if (_questionRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefault(q => q.Name.Equals(request.Question.Name)
                && q.QuestionCategoryId == request.Question.QuestionCategoryId)
                    != null)
                {
                    return false;
                }
                await _questionRepository.AddAsync(request.Question);
            }
            else
            {
                questionnaire.Name = request.Question.Name;
                questionnaire.QuestionCategoryId = request.Question.QuestionCategoryId;
            }
            await _questionRepository.SaveAsync();
            return true;
        }
    }
}
