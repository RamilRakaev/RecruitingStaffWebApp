
using System;

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

        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime();
        public string Address { get; set; } = string.Empty;
        public string DocumentSource { get; set; } = string.Empty;
    }
}
