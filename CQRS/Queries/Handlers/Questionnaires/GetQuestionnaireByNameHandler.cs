using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questionnaires
{
    public class GetQuestionnaireByNameHandler : IRequestHandler<GetQuestionnaireByNameQuery, Questionnaire>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public GetQuestionnaireByNameHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<Questionnaire> Handle(GetQuestionnaireByNameQuery request, CancellationToken cancellationToken)
        {
            return await _questionnaireRepository
                .GetAllAsNoTracking()
                .Where(q => q.Name.Equals(request.Name))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
