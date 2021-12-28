using System.Collections.Generic;

namespace RecruitingStaffWebApp.Services.DocParse.Model
{
    public class QuestionnaireElement
    {
        public QuestionnaireElement()
        {
            ChildElements = new();
        }
    
        public QuestionnaireElement(string name)
        {
            Name = name;
            ChildElements = new();
        }

        public string Name { get; private set; }
        public Dictionary<string, string> Properties = new();
        public List<QuestionnaireElement> ChildElements { get; private set; }
        public QuestionnaireElement CurrentChildElement { get; private set; }
        public int Count { get { return ChildElements.Count; } }

        public bool AddChildElement(QuestionnaireElement element)
        {
            if(element != null)
            {
                CurrentChildElement = element;
                ChildElements.Add(element);
                return true;
            }
            return false;
        }

        public void AddRangeElements(IEnumerable<QuestionnaireElement> elements)
        {
            ChildElements.AddRange(elements);
        }

        public QuestionnaireElement CreateChildElement(string name)
        {
            CurrentChildElement = CreateQuestionnaireElement(name);
            ChildElements.Add(CurrentChildElement);
            return CurrentChildElement;
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
