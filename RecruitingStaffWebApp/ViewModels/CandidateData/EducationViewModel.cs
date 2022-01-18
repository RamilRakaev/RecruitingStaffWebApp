using System;

namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class EducationViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndDateOfTraining { get; set; }

        public string Specialization { get; set; }
        public string Qualification { get; set; }

        public int CandidateId { get; set; }
    }
}
