using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class QuestionnaireElement
    {
        public QuestionnaireElement()
        {
            ChildElements = new();
        }

        public string Name { get; set; }
        public Dictionary<string, string> Properties = new();
        public List<QuestionnaireElement> ChildElements { get; private set; }
    }
}
