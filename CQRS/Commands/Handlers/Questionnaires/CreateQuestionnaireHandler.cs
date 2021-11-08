using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class CreateQuestionnaireHandler : IRequestHandler<CreateQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public CreateQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(CreateQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            await _questionnaireRepository.AddAsync(request.Questionnaire);
            await _questionnaireRepository.SaveAsync();
            return true;
        }
    }
}
