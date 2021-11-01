using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Vacancy : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }

        public List<CandidateVacancy> CandidateVacancies { get; set; }
        public List<Questionnaire> Questionnaires { get; set; }

        [NotMapped]
        public string[] ResponsibilitiesList
        {
            get
            {
                if(Responsibilities != null)
                {
                    return Responsibilities.Split("\n");
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }

        [NotMapped]
        public string[] ReuirementsList
        {
            get
            {if(Requirements != null)
                {
                    return Requirements.Split("\n");
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }

        [NotMapped]
        public string[] WorkingConditionsList
        { 
            get 
            { 
                if(WorkingConditions != null)
                {
                    return WorkingConditions.Split("\n");
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }
    }
}
