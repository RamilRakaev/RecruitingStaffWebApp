using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questions
{
    public class ChangeQuestionHandler : IRequestHandler<ChangeQuestionCommand, bool>
    {
        private readonly IRepository<Question> _questionRepository; 
        
        public ChangeQuestionHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(ChangeQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindAsync(request.Question.Id);
            question.Name = request.Question.Name;
            await _questionRepository.SaveAsync();
            return true;
        }
    }
}
