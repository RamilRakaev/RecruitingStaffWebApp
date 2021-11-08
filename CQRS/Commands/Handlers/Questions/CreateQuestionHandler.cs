using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questions
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, bool>
    {
        private readonly IRepository<Question> _questionRepository; 
        
        public CreateQuestionHandler(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            await _questionRepository.AddAsync(request.Question);
            await _questionRepository.SaveAsync();
            return true;
        }
    }
}
