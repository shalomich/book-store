using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record AlbumForm
    {
        [Required]
        public string TitleImageName { init; get; }

        [Required]
        public ISet<ImageForm> Images { init; get; }
    }
}
