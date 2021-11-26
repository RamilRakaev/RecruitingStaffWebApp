using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateOrChangeQuestionnaireHandler : IRequestHandler<CreateOrChangeQuestionnaireCommand, Questionnaire>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public CreateOrChangeQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<Questionnaire> Handle(CreateOrChangeQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository
                .FindAsync(request.Questionnaire.Id, cancellationToken)
                ?? await _questionnaireRepository
                .GetAllAsNoTracking()
                .Where(q => q.Name.Equals(request.Questionnaire.Name))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (questionnaire == null)
            {
                await _questionnaireRepository.AddAsync(request.Questionnaire, cancellationToken);
                await _questionnaireRepository.SaveAsync(cancellationToken);
                return request.Questionnaire;
            }
            else
            {
                request.Questionnaire.Id = questionnaire.Id;
                await _questionnaireRepository.Update(request.Questionnaire);
                await _questionnaireRepository.SaveAsync(cancellationToken);
            }
            return questionnaire;
        }
    }
}
