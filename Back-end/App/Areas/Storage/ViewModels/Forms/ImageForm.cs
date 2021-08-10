using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record ImageForm
    {
        [Required]
        public string Name { init; get; }

        [Required]
        public string Format { init; get; }

        [Required]
        public string Data { init; get; }
    }
}
