using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Vacancy : CandidateQuestionnaireEntity
    {
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }

        public List<CandidateVacancy> CandidateVacancy { get; set; }
        public List<Questionnaire> Questionnaires { get; set; }
    }
}
