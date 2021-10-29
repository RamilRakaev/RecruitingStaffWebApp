using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Candidate : BaseEntity
    {
        public Candidate()
        {

        }

        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

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
