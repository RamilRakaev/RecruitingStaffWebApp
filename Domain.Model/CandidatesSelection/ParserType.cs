using System.ComponentModel.DataAnnotations;

namespace RecruitingStaff.Domain.Model.CandidatesSelection
{
    public enum ParserType
    {
        [Display(Name = "Парсер анкеты C# разработчика")]
        CSharpDeveloperParser,

        [Display(Name = "Парсер анкеты php разработчика")]
        PhpDeveloperParser,

        [Display(Name = "Парсер анкеты офисного работника")]
        OfficeParser,

        [Display(Name = "Парсер анкеты девопса")]
        DevOpsParser
    }
}
