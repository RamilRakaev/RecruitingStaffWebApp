using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class ParsedDataCheck
    {
        private readonly string[] _requiredProperties;

        public ParsedDataCheck(string[] requiredProperties)
        {
            ExceptionMessages = new List<string>();
            _requiredProperties = requiredProperties;
        }

        public List<string> ExceptionMessages { get; set; }

        public bool Checking(ParsedData parsedData)
        {
            var parseDataProperties = parsedData.GetType().GetProperties();
            foreach (var parseDataPropertyType in parseDataProperties)
            {
                var value = parseDataPropertyType.GetValue(parsedData);
                if (parseDataPropertyType.GetValue(parsedData) == null)
                {
                    ExceptionMessages.Add("Неправильно введена анкета");
                    return false;
                }
            }

            foreach (var parseDataPropertyType in parseDataProperties)
            {
                foreach (var property in parseDataPropertyType
                    .GetValue(parsedData)
                    .GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(string)))
                {
                    if (_requiredProperties.Contains(property.Name))
                    {
                        var parseDataProperty = parseDataPropertyType.GetValue(parsedData);
                        var value = property.GetValue(parseDataProperty);
                        if (property.GetValue(parseDataProperty) as string == string.Empty)
                        {
                            ExceptionMessages.Add($"Свойство {property.Name} не определено");
                        }
                    }
                }
            }
            return ExceptionMessages.Count == 0;
        }
    }
}
