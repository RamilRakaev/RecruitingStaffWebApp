using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace RecruitingStaff.WebApp.ViewModels.CandidateData
{
    public class CandidateViewModel : BaseViewModel
    {
        public CandidateViewModel()
        {

        }

        public CandidateViewModel(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public CandidateViewModel(SelectList vacanciesSelectList)
        {
            VacanciesSelectList = vacanciesSelectList;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string AddressIndex { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }

        public List<PreviousJobPlacementViewModel> PreviousJobs { get; set; }
        public List<string> Educations { get; set; }
        public List<string> Kids { get; set; }

        public int[] VacancyIds { get; set; }
        public SelectList VacanciesSelectList { get; set; }

        public int QuestionnaireId { get; set; }
    }
}
