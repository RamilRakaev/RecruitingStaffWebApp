using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class QuestionnaireElement
    {
        public string Name { get; set; }
        public QuestionnaireElement Parent { get; set; }
        public List<QuestionnaireElement> ChildElements { get; set; }
    }
}
