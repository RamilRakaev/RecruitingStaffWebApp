
namespace Domain.Model
{
    public class Vacancy : BaseEntity
    {
        public string Name { get; set; }
        public string Responsibilities { get; set; }
        public string WorkingConditions { get; set; }
        public string Requirements { get; set; }
    }
}
