using System;

namespace RecruitingStaff.WebApp.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        public VacancyViewModel()
        {

        }

        public VacancyViewModel(object obj) : base(obj)
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string WorkingConditions { get; set; }

        public string[] ResponsibilitiesList
        {
            get
            {
                if (Responsibilities != null)
                {
                    return Responsibilities.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }

        public string[] RequirementsList
        {
            get
            {
                if (Requirements != null)
                {
                    return Requirements.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return Array.Empty<string>();
                }
            }
        }

        public string[] WorkingConditionsList
        {
            get
            {
                if (WorkingConditions != null)
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
