﻿namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Kid : CandidateQuestionnaireEntity
    {
        public string Gender { get; set; }
        public int Age { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}