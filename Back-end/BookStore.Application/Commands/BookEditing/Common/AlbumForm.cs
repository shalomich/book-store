using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookStore.Application.Commands.BookEditing.Common;
 public record AlbumForm : IValidatableObject
{
    [Required]
    public string TitleImageName { init; get; }

    [Required, MinLength(Album.MinImageCount), MaxLength(Album.MaxImageCount)]
    public IEnumerable<ImageForm> Images { init; get; } = new List<ImageForm>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool hasTitleImage = Images.Any(image => image.Name == TitleImageName);

        if (!hasTitleImage)
        {
            yield return new ValidationResult("There is no image with titleImageName.");
        }

        if (Images.Distinct().Count() != Images.Count())
        {
            yield return new ValidationResult("Image names should not be repeated.");
        }
    }
}
