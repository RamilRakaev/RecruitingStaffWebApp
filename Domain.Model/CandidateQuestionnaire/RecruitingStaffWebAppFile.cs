
namespace RecruitingStaff.Domain.Model.CandidateQuestionnaire
{
    public enum FileType { Questionnaire, Photo}

    public class RecruitingStaffWebAppFile : BaseEntity
    {
        public string Source { get; set; }
        public FileType FileType { get; set; }
        public Candidate Candidate { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
