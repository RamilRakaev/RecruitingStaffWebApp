﻿using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Answers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.Answers
{
    public class GetAnswersByCanidateIdHandler : IRequestHandler<GetAnswersByCanidateIdQuery, Answer[]>
    {
        private readonly IRepository<Answer> _answerRepository;

        public GetAnswersByCanidateIdHandler(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public Task<Answer[]> Handle(GetAnswersByCanidateIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_answerRepository.GetAllAsNoTracking().Where(a => a.CandidateId == request.CandidateId).ToArray());
        }
    }
}