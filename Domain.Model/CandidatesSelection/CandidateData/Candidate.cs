using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using System;
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData
{
    public class Candidate : CandidateQuestionnaireEntity
    {
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string AddressIndex { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }

        public List<CandidateQuestionnaire> CandidateQuestionnaire { get; set; }
        public List<CandidateVacancy> CandidateVacancy { get; set; }
        public List<CandidateTestTask> CandidateTestTasks { get; set; }

        public List<Option> Options { get; set; }
        public List<Answer> Answers { get; set; }

        public int? PhotoId { get; set; }
        public RecruitingStaffWebAppFile Photo { get; set; }

        public List<RecruitingStaffWebAppFile> Documents { get; set; }
        public List<PreviousJobPlacement> PreviousJobs { get; set; }
        public List<Education> Educations { get; set; }
        public List<Kid> Kids { get; set; }
    }
}
