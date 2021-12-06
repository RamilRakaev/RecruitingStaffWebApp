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
            var questionCategory = await _questionCategoryRepository
                .FindAsync(request.QuestionCategory.Id, cancellationToken) ??
                await _questionCategoryRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefaultAsync(q => q.Name.Equals(request.QuestionCategory.Name)
                    && q.QuestionnaireId == request.QuestionCategory.QuestionnaireId, cancellationToken);
            if(questionCategory == null)
            {
                request.QuestionCategory.Questionnaire = null;
                await _questionCategoryRepository.AddAsync(request.QuestionCategory, cancellationToken);
            }
            else
            {
                request.QuestionCategory.Id = questionCategory.Id;
                await _questionCategoryRepository.Update(request.QuestionCategory);
            }
            await _questionCategoryRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
