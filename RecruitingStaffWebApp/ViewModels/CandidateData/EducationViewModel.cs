using RecruitingStaff.WebApp.ViewModels;
using System;

namespace RecruitingStaff.WebApp.Validators
{
    public class EducationViewModel : BaseViewModel
    {
        public EducationViewModel()
        {

        }

        public EducationViewModel(object obj) : base(obj)
        {

        }

        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string EducationalInstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }
    }
}
