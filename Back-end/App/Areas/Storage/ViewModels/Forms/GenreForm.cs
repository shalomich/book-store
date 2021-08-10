using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record GenreForm : EntityForm
    {
        [Required]
        public string Name { init; get; }
    }
}
