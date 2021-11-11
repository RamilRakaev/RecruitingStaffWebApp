using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Questionnaires
{
    public class GetQuestionnairesByNameFragmentHandler : IRequestHandler<GetQuestionnairesByNameFragmentQuery, Questionnaire[]>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public GetQuestionnairesByNameFragmentHandler(IRepository<Questionnaire> questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public Task<Questionnaire[]> Handle(GetQuestionnairesByNameFragmentQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = _questionnaireRepository.GetAllAsNoTracking().Where(q => q.Name.Contains(request.NameFragment)).ToArray();
            if(questionnaires == null)
            {
                return Task.FromResult(Array.Empty<Questionnaire>());
            }
            return Task.FromResult(questionnaires);
        }
    }
}
