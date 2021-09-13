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
        [Required]
        [FormField(FormFieldType.Text, "Имя")]
        [RegularExpression(Author.NameMask, ErrorMessage = Author.NameSchema)]
        public string Name { init; get; }
    }
}
