using App.Attributes.FormModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    [FormModel]
    public record GenreForm : EntityForm
    {
        [Required]
        [FormField(FormFieldType.Text, "Жанр")]
        public string Name { init; get; }
    }
}
