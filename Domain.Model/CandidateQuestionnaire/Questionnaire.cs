
namespace Domain.Model.CandidateQuestionnaire
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }
        public int? VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
