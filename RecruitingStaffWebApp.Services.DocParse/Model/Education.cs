using System;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class Education
    {
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string EducationalInstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }
    }
}
