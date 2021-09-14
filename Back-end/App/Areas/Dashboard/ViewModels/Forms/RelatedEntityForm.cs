using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels.Forms
{
    public record RelatedEntityForm : IEntityForm
    {
        public int Id { init; get; }

        [Required]
        public string Name { init; get; }
    }
}
