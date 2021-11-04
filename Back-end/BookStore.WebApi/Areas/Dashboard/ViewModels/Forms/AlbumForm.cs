using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public record AlbumForm
    {
        private const string InvalidTitleImageName = "This is not image with name as album title image name";
        [Required]
        public string TitleImageName { init; get; }

        [Required, MinLength(Album.MinImageCount), MaxLength(Album.MaxImageCount)]
        public ISet<ImageForm> Images { init; get; }

        public AlbumForm(string titleImageName, ISet<ImageForm> images)
        {
            if (images.Any(image => image.Name == titleImageName) == false)
                throw new BadRequestException(InvalidTitleImageName);

            TitleImageName = titleImageName;
            Images = images;
        }
    }
}
