
using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Contender : BaseEntity
    {
        public Contender()
        {

        }

        public Contender(string documentSource)
        {
            DocumentSource = documentSource;
        }

        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string DocumentSource { get; set; }
        public List<Option> Options { get; set; }
    }
}
