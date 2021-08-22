using App.Attributes.FormModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    [FormModel]
    public record AgeLimitForm : EntityForm
    {
        [Required]
        [FormField(FormFieldType.Text,"Возрастное ограничение")]
        public string Name { init; get; }
    }
}
