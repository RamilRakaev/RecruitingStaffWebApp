using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using System.Collections.Generic;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class Vacancy : CandidatesSelectionEntity 
    {
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }

        public List<CandidateVacancy> CandidateVacancy { get; set; }
        public List<VacancyQuestionnaire> VacancyQuestionnaire { get; set; }
        public List<TestTask> TestTasks { get; set; }
    }
}
