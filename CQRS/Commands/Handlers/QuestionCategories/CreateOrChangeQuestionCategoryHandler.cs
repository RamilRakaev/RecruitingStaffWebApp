using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.QuestionCategories
{
    public class CreateOrChangeQuestionCategoryHandler : IRequestHandler<CreateOrChangeQuestionCategoryCommand, bool>
    {
        private readonly IRepository<QuestionCategory> _questionnaireRepository;

        public CreateOrChangeQuestionCategoryHandler(IRepository<QuestionCategory> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(CreateOrChangeQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository.FindAsync(request.QuestionCategory.Id, cancellationToken);
            if (questionnaire == null)
            {
                if (await _questionnaireRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefaultAsync(q => q.Name.Equals(request.QuestionCategory.Name)
                    && q.QuestionnaireId == request.QuestionCategory.QuestionnaireId, cancellationToken)
                    != null)
                {
                    return false;
                }
                await _questionnaireRepository.AddAsync(request.QuestionCategory, cancellationToken);
            }
            else
            {
                questionnaire.Name = request.QuestionCategory.Name;
                questionnaire.QuestionnaireId = request.QuestionCategory.QuestionnaireId;
            }
            await _questionnaireRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
