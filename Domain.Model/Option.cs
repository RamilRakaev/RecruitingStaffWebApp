
namespace Domain.Model
{
    public class Option : BaseEntity
    {
        public string  PropertyName { get; set; }
        public string Value { get; set; }
        public int? ContenderId { get; set; }
        public Contender Contender { get; set; }
    }
}