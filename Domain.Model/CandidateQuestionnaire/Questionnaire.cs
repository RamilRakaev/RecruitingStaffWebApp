using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public List<CandidateQuestionnaire> CandidateQuestionnaires { get; set; }
        public List<RecruitingStaffWebAppFile> DocumentFiles { get; set; }
        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}
