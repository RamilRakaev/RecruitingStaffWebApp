using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class QuestionnaireParsedData
    {
        public string Name { get; set; }
        public List<string> QuestionCategories { get; set; }
        public List<string> Questions { get; set; }
        public List<string> Answers { get; set; }
    }
}
