using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class Questionnaire : CandidatesSelectionEntity 
    {
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public List<CandidateQuestionnaire> CandidatesQuestionnaire { get; set; }
        public List<RecruitingStaffWebAppFile> DocumentFiles { get; set; }
        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}
