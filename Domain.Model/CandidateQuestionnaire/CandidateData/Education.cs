﻿using System;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData
{
    public class Education : BaseEntity
    {
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
