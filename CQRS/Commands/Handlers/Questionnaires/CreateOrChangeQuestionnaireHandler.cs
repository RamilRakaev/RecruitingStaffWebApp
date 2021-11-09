using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateOrChangeQuestionnaireHandler : IRequestHandler<CreateOrChangeQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public CreateOrChangeQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(CreateOrChangeQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository.FindAsync(request.Questionnaire.Id);
            if(questionnaire == null)
            {
                await _questionnaireRepository.AddAsync(request.Questionnaire);
            }
            else
            {
                questionnaire.Name = request.Questionnaire.Name;
            }
            await _questionnaireRepository.SaveAsync();
            return true;
        }
    }
}
