using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using System.Collections.Generic;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public class TestTask : CandidateQuestionnaireEntity
    {
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }

        public List<CandidateTestTask> CandidateTestTasks { get; set; }

        public RecruitingStaffWebAppFile DocumentFile { get; set; }
    }
}
