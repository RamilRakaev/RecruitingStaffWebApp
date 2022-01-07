using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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

        public int[] VacancyIds { get; set; }
        public SelectList VacanciesSelectList { get; set; }

        public int QuestionnaireId { get; set; }
    }
}
