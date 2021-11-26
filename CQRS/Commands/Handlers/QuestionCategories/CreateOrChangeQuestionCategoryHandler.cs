using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class CreateOrChangeQuestionCategoryHandler : IRequestHandler<CreateOrChangeQuestionCategoryCommand, bool>
    {
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;

        public CreateOrChangeQuestionCategoryHandler(IRepository<QuestionCategory> questionnaireRepository)
        {
            _questionCategoryRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(CreateOrChangeQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionCategoryRepository.FindAsync(request.QuestionCategory.Id, cancellationToken);
            if (questionnaire == null)
            {
                var category = await _questionCategoryRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefaultAsync(q => q.Name.Equals(request.QuestionCategory.Name)
                    && q.QuestionnaireId == request.QuestionCategory.QuestionnaireId, cancellationToken);
                if (category != null)
                {
                    request.QuestionCategory.Id = category.Id;
                    return false;
                }
                request.QuestionCategory.Questionnaire = null;
                await _questionCategoryRepository.AddAsync(request.QuestionCategory, cancellationToken);
            }
            else
            {
                questionnaire.Name = request.QuestionCategory.Name;
                questionnaire.QuestionnaireId = request.QuestionCategory.QuestionnaireId;
            }
            await _questionCategoryRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
