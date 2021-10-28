
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Contender : BaseEntity
    {
        public Contender()
        {

        }

        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
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
