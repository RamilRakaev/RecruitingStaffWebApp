
namespace Domain.Model.CandidateQuestionnaire
{
    public class Question : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public QuestionCategory Category { get; set; }
    }
}
