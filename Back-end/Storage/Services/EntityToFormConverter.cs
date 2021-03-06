using Microsoft.Extensions.Configuration;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Services
{
    public class EntityToFormConverter
    {
        private class PropertyToFormElement
        {
            public readonly Type PropertyType;
            
            public readonly string[] FormElements;
            
            public readonly string DefaultFormElement;
            public PropertyToFormElement(Type propertyType, string[] formElements, string defaultElement)
            {
                PropertyType = propertyType;
                FormElements = formElements;

                if (FormElements.Contains(defaultElement) == false)
                    throw new ArgumentException();
                DefaultFormElement = defaultElement;
            }
        }
        

        private const string _entitySection = "form:entity";
        private const string _settingsSection = "form:settings";
        
        private readonly ImmutableArray<PropertyToFormElement> _defaultSettings;
        private readonly Dictionary<string, Dictionary<string, string>> _entityConfigSettings;
        public EntityToFormConverter(IConfiguration configuration) 
        {
            _defaultSettings = configuration
                .GetSection(_settingsSection)
                .GetChildren()
                .Select(section => new PropertyToFormElement(
                    Type.GetType(section["propertyType"]),
                    section.GetSection("formElements")
                            .GetChildren()
                            .Select(section => section.Value)
                            .ToArray(),
                    section["defaultFormElement"]))
                .ToImmutableArray();
            _entityConfigSettings = configuration
                .GetSection(_entitySection)
                .GetChildren()
                .ToDictionary(
                    entitySection => entitySection.Key,
                    entitySection => entitySection
                    .GetChildren()
                    .ToDictionary(
                        propertySection => propertySection.Key,
                        propertySection => propertySection.Value));                                              
        }

        public Dictionary<string,string> Convert<T>() where T : Entity
        {
            var convertedType = typeof(T);

            var propertyConfigSettings = _entityConfigSettings[convertedType.Name];
            
            var properties = convertedType.GetProperties();

            var finalSettings = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                var propertyToFormElement = _defaultSettings.FirstOrDefault(propertyToFormElement => propertyToFormElement.PropertyType == property.PropertyType);
                if (propertyToFormElement == null)
                    continue;


                if (propertyConfigSettings.TryGetValue(property.Name, out string formElement) == true)
                    if (propertyToFormElement.FormElements.Contains(formElement) == true)
                        finalSettings.Add(property.Name, formElement);
                    else finalSettings.Add(property.Name, propertyToFormElement.DefaultFormElement);
                else finalSettings.Add(property.Name, propertyToFormElement.DefaultFormElement);
            }

            return finalSettings;
        }
    }
}
