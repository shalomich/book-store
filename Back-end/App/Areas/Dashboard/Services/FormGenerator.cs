using App.Attributes.FormModel;
using Store.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Services
{
    public class FormGenerator
    {
        public record FormField(string Name, string Text, string Type, bool IsRequired);

        public IEnumerable<FormField> Convert(Type formModelType)
        {
            if (formModelType.GetCustomAttributes<FormModelAttribute>() == null)
                throw new ArgumentException();

            PropertyInfo[] properties = formModelType.GetProperties();

            var formTemplate = new List<FormField>();

            foreach (var property in properties)
            {
                var fieldAttribute = property
                    .GetCustomAttribute<FormFieldAttribute>();

                if (fieldAttribute == null)
                    continue;

                string name = property.Name.ToLowFirstLetter();

                formTemplate.Add(
                    new FormField(
                        Name: name,
                        Text: fieldAttribute.Text,
                        Type: fieldAttribute.Type.ToString().ToLower(),
                        IsRequired: fieldAttribute.IsRequired
                    ));
            }

            return formTemplate;
        }
    }
}
