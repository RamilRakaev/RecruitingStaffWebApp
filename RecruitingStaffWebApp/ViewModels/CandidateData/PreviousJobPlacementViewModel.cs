using System;
using System.Collections.Generic;

namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class PreviousJobPlacementViewModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PositionAtWork { get; set; }
        public string Salary { get; set; }

        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }

        public string Responsibilities { get; set; }

        public string LeavingReason { get; set; }
        public List<string> RecommenderNames { get; set; }
        public string RecommendersToString
        {
            get
            {
                if (RecommenderNames != null)
                {
                    return string.Join(", ", RecommenderNames);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
