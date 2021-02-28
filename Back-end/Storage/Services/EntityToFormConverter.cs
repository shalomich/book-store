using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Services
{
    public class EntityToFormConverter
    {
        private Dictionary<Type, ImmutableArray<string>> TypeToFormElements => new Dictionary<Type, ImmutableArray<string>>()
        {
            {typeof(string), new string[]
                {
                    "text",
                    "textArea",
                    "password",
                    "tel",
                    "email"
                }.ToImmutableArray()
            },
            {
                typeof(int), new string[]
                {
                    "number"
                }.ToImmutableArray()
            },
            {
                typeof(DateTime), new string[]
                {
                    "date",
                    "time"
                }.ToImmutableArray()
            },
            {
                typeof(IEnumerable<string>),new string[]
                {
                    "select",
                    "select multiple"
                }.ToImmutableArray()
            }

        };

        private Dictionary<Type, string> DefaultTypeToFormElements => new Dictionary<Type, string>()
        {
            {typeof(string), "text"},
            {typeof(int), "number"},
            {typeof(DateTime), "date"},
            {typeof(IEnumerable<string>), "select"}
        };


        public Dictionary<string,string> Convert<T>(Dictionary<string,string> propertyToFormElements = null) where T : new()
        {
            var convertedType = new T().GetType();
        
            Console.WriteLine(convertedType.Name);
            Console.WriteLine(convertedType.FullName);
            var properties = convertedType.GetProperties();

            var fullPropertyToFormElements = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                if (TypeToFormElements.ContainsKey(property.PropertyType) == false)
                    continue;

                string formElement = null;
                if (propertyToFormElements?.TryGetValue(property.Name, out formElement) == true)
                {
                    var formElements = TypeToFormElements[property.PropertyType];
                    if (formElements.Contains(formElement) == false)
                        formElement = DefaultTypeToFormElements[property.PropertyType];
                }
                else formElement = DefaultTypeToFormElements[property.PropertyType];

                fullPropertyToFormElements.Add(property.Name, formElement);
            }

            return fullPropertyToFormElements;
        }
    }
}
