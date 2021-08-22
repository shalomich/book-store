using App.Attributes.FormModel;
using App.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    [FormModel]
    public record AuthorForm : EntityForm
    {
        private const string NameTemplate = "^[А-ЯЁ][а-яё]*$";

        [JsonIgnore]
        public string Name => string.Join(' ', Surname, FirstName, Patronymic);

        [Required]
        [RegularExpression(NameTemplate)]
        [FormField(FormFieldType.Text,"Имя")]
        public string FirstName { init; get; }

        [Required]
        [RegularExpression(NameTemplate)]
        [FormField(FormFieldType.Text,"Фамилия")]
        public string Surname { init; get; }

        [RegularExpression(NameTemplate)]
        [FormField(FormFieldType.Text, "Отчество",false)]
        public string Patronymic { init; get; }
        
    }
}
