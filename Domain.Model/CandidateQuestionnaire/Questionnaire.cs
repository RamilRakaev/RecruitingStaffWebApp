using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public int DocumentFileId { get; set; }
        public RecruitingStaffWebAppFile DocumentFile { get; set; }

        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}
