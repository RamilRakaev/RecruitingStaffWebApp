using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.Maps
{
    public class VacancyQuestionnaire : BaseMap
    {
        public Vacancy Vacancy { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
