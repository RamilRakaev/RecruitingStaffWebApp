using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string MaritalStatus { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public int? VacancyId { get; set; }
        public Vacancy VacancyClaim  { get; set; }

        [NotMapped]
        public string DocumentSource
        {
            get
            {
                return $"{Id}.{FullName}.docx";
            }
        }
        public List<Option> Options { get; set; }
    }
}
