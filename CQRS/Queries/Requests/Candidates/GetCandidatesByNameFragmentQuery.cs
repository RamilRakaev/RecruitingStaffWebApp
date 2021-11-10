﻿using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates
{
    public class GetCandidatesByNameFragmentQuery : IRequest<Candidate[]>
    {
        public GetCandidatesByNameFragmentQuery(string nameFragment)
        {
            NameFragment = nameFragment;
        }

        public string NameFragment { get; set; }
    }
}
