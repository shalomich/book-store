using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public record ImageForm
    {
        [Required]
        public string Name { init; get; }

        [Required]
        public string Format { init; get; }

        [Required]
        public string Data { init; get; }

        [Required]
        [Range(Image.MinHeight, int.MaxValue)]
        public int Height { init; get; }

        [Required]
        [Range(Image.MinWidth, int.MaxValue)]
        public int Width { init; get; }
    }
}
