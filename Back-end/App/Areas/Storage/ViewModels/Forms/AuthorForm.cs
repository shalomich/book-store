using App.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record AuthorForm : EntityForm
    {
        private const string NameTemplate = "^[А-ЯЁ][а-яё]*$";

        [JsonIgnore]
        public string Name => string.Join(' ', Surname, FirstName, Patronymic);

        [Required]
        [RegularExpression(NameTemplate)]
        public string FirstName { init; get; }

        [Required]
        [RegularExpression(NameTemplate)]
        public string Surname { init; get; }

        [RegularExpression(NameTemplate)]
        public string Patronymic { init; get; }
        
    }
}
