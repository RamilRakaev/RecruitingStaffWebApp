using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitingStaff.WebApp.ViewModels.Questionnaire
{
    public class QuestionnaireViewModel : BaseViewModel
    {
        public QuestionnaireViewModel()
        {

        }

        public QuestionnaireViewModel(object obj) : base(obj)
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public QuestionCategoryViewModel[] QuestionCategories { get; set; }
        public SelectList ParserTypesSelectList { get; set; }
        public int ParserTypeIndex { get; set; }
        public SelectList VacanciesSelectList { get; set; }
        public int VacancyId { get; set; }
    }
}
