
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
        public string Address { get; set; }
        public string DocumentSource { get; set; }
    }
}
