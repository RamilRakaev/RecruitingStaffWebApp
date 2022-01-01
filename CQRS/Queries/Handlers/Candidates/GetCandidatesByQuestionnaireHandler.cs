using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Candidates
{
    public class GetCandidatesByQuestionnaireHandler : IRequestHandler<GetCandidatesByQuestionnaireQuery, Candidate[]>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;

        public GetCandidatesByQuestionnaireHandler(IRepository<Candidate> candidateRepository,
            IRepository<CandidateQuestionnaire> candidateQuestionnaireRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateQuestionnaireRepository = candidateQuestionnaireRepository;
        }

        public async Task<Candidate[]> Handle(GetCandidatesByQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            var candidateQuestionnaires = _candidateQuestionnaireRepository
                .GetAllAsNoTracking()
                .Where(cq => cq.SecondEntityId == request.QuestionnaireId)
                .Select(cq => cq.FirstEntityId);
            var candidates = await _candidateRepository
                .GetAllAsNoTracking()
                .Where(c => candidateQuestionnaires
                .Contains(c.Id))
                .ToArrayAsync(cancellationToken);
            return candidates;
        }
    }
}