using System;
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string MaritalStatus { get; set; }

        public List<CandidateQuestionnaire> CandidateQuestionnaires { get; set; }
        public List<CandidateVacancy> CandidateVacancies { get; set; }
        public List<Option> Options { get; set; }
        public List<Answer> Answers { get; set; }

        public int? PhotoId { get; set; }
        public RecruitingStaffWebAppFile Photo { get; set; }

        public List<RecruitingStaffWebAppFile> Documents { get; set; }
    }
}
