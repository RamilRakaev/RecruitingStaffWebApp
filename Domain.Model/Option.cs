﻿
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Domain.Model
{
    public class Option : BaseEntity
    {
        public string  PropertyName { get; set; }
        public string Value { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}