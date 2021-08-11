using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Attributes.FormModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormFieldAttribute : Attribute
    {
        public FormFieldType Type { get; }
        public string Text { get; }
        public bool IsRequired { get; }
        public FormFieldAttribute(FormFieldType type, string text, bool isRequired = true)
        {
            Type = type;
            IsRequired = isRequired;
            Text = text;
        }
    }
}
