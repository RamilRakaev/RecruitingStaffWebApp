using System.ComponentModel.DataAnnotations;

namespace RecruitingStaffWebApp.Services.DocParse
{
    public enum JobQuestionnaireType
    {
        [Display(Name = "Анкета C# разработчика")]
        CSharpDeveloperQuestionnaire,

        [Display(Name = "Анкета php разработчика")]
        PhpDeveloperQuestionnaire,

        [Display(Name = "Анкета офисного работника")]
        OfficeQuestionnaire,

        [Display(Name = "Анкета девопса")]
        DevOpsQuestionnaire
    }
}
