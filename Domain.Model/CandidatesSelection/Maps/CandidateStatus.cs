using System.ComponentModel.DataAnnotations;

namespace RecruitingStaff.Domain.Model.CandidatesSelection.Maps
{
    public enum CandidateStatus : int
    {
        [Display(Name = "Новый кандидат")]
        NewCandidate = 0,
        [Display(Name = "Анкета отправлена")]
        TheQuestionnaireHasBeenSent = 1,
        [Display(Name = "Тестовое задание отправлено")]
        TheTestTaskHasBeenSent = 2,
        [Display(Name = "Отказано")]
        WorkDenied = 3,
        [Display(Name = "Ожидает собеседования")]
        AwaitingAnInterview = 4
    }
}
