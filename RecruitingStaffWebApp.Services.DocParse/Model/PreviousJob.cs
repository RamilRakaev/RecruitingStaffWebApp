using System;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class PreviousJobParsedData
    {
        public string OrganizationName { get; set; }
        public string OrganizationPhoneNumber { get; set; }
        public string OrganizationAddress { get; set; }
        public string PositionAtWork { get; set; }
        public string Salary { get; set; }

        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }

        public string Responsibilities { get; set; }

        public string LeavingReason { get; set; }

        public List<Recommender> Recommenders { get; set; }
    }
}
