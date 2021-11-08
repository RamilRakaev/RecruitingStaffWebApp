using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class ChangeQuestionnaireHandler : IRequestHandler<ChangeQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public ChangeQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(ChangeQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var oldQuestionnaire = await _questionnaireRepository.FindAsync(request.Questionnaire.Id);
            oldQuestionnaire.Name = request.Questionnaire.Name;
            await _questionnaireRepository.SaveAsync();
            return true;
        }
    }
}
