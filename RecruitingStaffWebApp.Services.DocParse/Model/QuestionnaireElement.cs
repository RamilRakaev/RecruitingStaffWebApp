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
        public QuestionnaireElement CurrentElement { get; private set; }

        public void AddChildElement(QuestionnaireElement element)
        {
            CurrentElement = element;
            ChildElements.Add(element);
        }

        public static QuestionnaireElement CreateQuestionnaireElement(string name)
        {
            QuestionnaireElement questionnaireElement = new();
            questionnaireElement.Name = name;
            questionnaireElement.Properties.Add("Name", name);
            return questionnaireElement;
        }

        public static QuestionnaireElement CreateQuestionnaireElement(Dictionary<string, string> properties)
        {
            QuestionnaireElement questionnaireElement = new();
            questionnaireElement.Properties = properties;
            return questionnaireElement;
        }
    }
}
