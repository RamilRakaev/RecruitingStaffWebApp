using System;
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class PreviousJobPlacement : CandidateDataEntity
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PositionAtWork { get; set; }
        public string Salary { get; set; }

        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }

        public string Responsibilities { get; set; }

        public string LeavingReason { get; set; }
        public List<Recommender> Recommenders { get; set; }
    }
}
