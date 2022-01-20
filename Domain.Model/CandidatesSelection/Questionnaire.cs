using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using System.Collections.Generic;
using RecruitingStaff.Domain.Model.BaseEntities;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class Questionnaire : CandidatesSelectionEntity
    {
        public VacancyParserType ParserType { get; set; }
        public List<VacancyQuestionnaire> VacancyQuestionnaires { get; set; }
        public List<CandidateQuestionnaire> CandidatesQuestionnaire { get; set; }
        public List<RecruitingStaffWebAppFile> DocumentFiles { get; set; }
        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}
