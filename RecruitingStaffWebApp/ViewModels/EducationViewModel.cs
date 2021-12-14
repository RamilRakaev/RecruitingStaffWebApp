using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Validators
{
    public class EducationViewModel
    {
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string EducationalInstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }
    }
}
