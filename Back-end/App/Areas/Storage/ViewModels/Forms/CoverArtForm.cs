using App.Areas.Storage.Attributes.FormModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    [FormModel]
    public record CoverArtForm : EntityForm
    {
        [Required]
        [FormField(FormFieldType.Text,"Тип обложки")]
        public string Name { init; get; }
    }
}
