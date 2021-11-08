using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class CreateQuestionCategoryHandler : IRequestHandler<CreateQuestionCategoryCommand, bool>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public CreateQuestionCategoryHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<bool> Handle(CreateQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            await _questionCategoryRepository.AddAsync(request.QuestionCategory);
            await _questionCategoryRepository.SaveAsync();
            return true;
        }
    }
}
