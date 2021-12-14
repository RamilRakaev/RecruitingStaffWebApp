using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
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
        //private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaireRepository;

        public GetCandidatesByQuestionnaireHandler(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
            //_candidateQuestionnaireRepository = candidateQuestionnaireRepository;
        }

        public async Task<Candidate[]> Handle(GetCandidatesByQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            //var candidateQuestionnaires = _candidateQuestionnaireRepository
            //    .GetAllAsNoTracking()
            //    .Where(cq => cq.QuestionnaireId == request.QuestionnaireId)
            //    .Select(cq => cq.CandidateId);
            //var candidates = _candidateRepository.GetAllAsNoTracking();
            //return await candidates.Where(c => candidateQuestionnaires.Contains(c.Id)).ToArrayAsync();
            return null;
        }
    }
}
