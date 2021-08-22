using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Common.ViewModels
{
    public record AlbumDto
    {
        [Required]
        public string TitleImageName { init; get; }

        [Required]
        public ISet<ImageDto> Images { init; get; }
    }
}
