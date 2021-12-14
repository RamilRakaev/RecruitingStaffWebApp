using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public class Vacancy : BaseEntity
    {
        //public string Name { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }

        public List<Candidate> CandidateVacancies { get; set; }
        public List<Questionnaire> Questionnaires { get; set; }

        [NotMapped]
        public string[] ResponsibilitiesList
        {
            get
            {
                if(Responsibilities != null)
                {
                    return Responsibilities.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }

        [NotMapped]
        public string[] RequirementsList
        {
            get
            {if(Requirements != null)
                {
                    return Requirements.Split("\n", StringSplitOptions.RemoveEmptyEntries);
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
                    return WorkingConditions.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }
    }
}
