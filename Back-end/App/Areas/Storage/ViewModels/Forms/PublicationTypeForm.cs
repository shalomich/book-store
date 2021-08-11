using App.Areas.Storage.Attributes.FormModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    [FormModel]
    public record PublicationTypeForm : EntityForm
    {
        [Required]
        [FormField(FormFieldType.Text, "Тип издания")]
        public string Name { init; get; }
    }
}
