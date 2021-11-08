using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class ChangeQuestionCategoryHandler : IRequestHandler<ChangeQuestionCategoryCommand, bool>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public ChangeQuestionCategoryHandler(IRepository<QuestionCategory> questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<bool> Handle(ChangeQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionCategory = await _questionCategoryRepository.FindAsync(request.QuestionCategory.Id);
            questionCategory.Name = request.QuestionCategory.Name;
            await _questionCategoryRepository.SaveAsync();
            return true;
        }
    }
}
