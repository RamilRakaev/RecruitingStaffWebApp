using System;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class Candidate
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string AddressIndex { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }

        public string Photo { get; set; }
        public List<string> Documents { get; private set; } = new();

        public List<PreviousJob> PreviousJobs { get; set; }
        public List<Education> Educations { get; set; } = new();
        public List<Kid> Kids { get; set; }

        private PreviousJob currentPreviousJob;

        public void AddPreviousJob(PreviousJob previousJob)
        {
            currentPreviousJob = previousJob;
            PreviousJobs.Add(previousJob);
        }

        public void AddRecommender(Recommender recommender)
        {
            currentPreviousJob.Recommenders.Add(recommender);
        }
    }
}
