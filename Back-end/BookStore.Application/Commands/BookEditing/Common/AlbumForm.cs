using BookStore.Domain.Entities.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.BookEditing.Common;
 public record AlbumForm
{
    [Required]
    public string TitleImageName { init; get; }

    [Required, MinLength(Album.MinImageCount), MaxLength(Album.MaxImageCount)]
    public IEnumerable<ImageForm> Images { init; get; } = new List<ImageForm>();
}
