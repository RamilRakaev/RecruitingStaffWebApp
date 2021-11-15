using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questionnaires
{
    public class GetQuestionnaireHandler : IRequestHandler<GetQuestionnaireQuery, Questionnaire>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public GetQuestionnaireHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<Questionnaire> Handle(GetQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            return await _questionnaireRepository.FindAsync(request.QuestionnaireId, cancellationToken);
        }
    }
}
