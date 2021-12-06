using System;
using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class CandidateParsedData
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
        public List<string> Documents { get; set; }

        public List<PreviousJobParsedData> PreviousJobs { get; set; }
        public List<EducationParsedData> Educations { get; set; }
        public List<KidParsedData> Kids { get; set; }
    }
}
