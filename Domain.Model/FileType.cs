using System.ComponentModel.DataAnnotations;

namespace RecruitingStaff.Domain.Model
{
    public enum FileType {
        [Display(Name = "Пример анкеты")]
        QuestionnaireExample,

        [Display(Name = "Пройденная анкета")]
        CompletedQuestionnaire,

        [Display(Name = "Тестовое задание")]
        TestTask,

        [Display(Name = "Фото jpg формата")]
        JpgPhoto,

        [Display(Name = "Фото png формата")]
        PngPhoto
    }
}
