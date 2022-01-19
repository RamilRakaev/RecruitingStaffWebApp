using System;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Education : CandidateDataEntity
    {
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string Specialization { get; set; }
        public string Qualification { get; set; }
    }
}
