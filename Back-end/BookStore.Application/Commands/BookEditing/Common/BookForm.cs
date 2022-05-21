using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.BookEditing.Common;
public record BookForm
{
    #region Product
    public int Id { init; get; }

    [Required]
    public string Name { init; get; }

    [Required]
    [Range(Product.MinCost, int.MaxValue)]
    public int Cost { init; get; }

    [Required]
    [Range(Product.MinQuantity, int.MaxValue)]
    public int Quantity { init; get; }

    [MaxLength(Product.MaxDescriptionLength)]
    public string Description { init; get; }

    [Required]
    public AlbumForm Album { init; get; }

    public DiscountForm Discount { init; get; }
    public IEnumerable<int> TagIds { init; get; } = new List<int>();

    #endregion

    #region Book

    [Required]
    [RegularExpression(Book.IsbnTemplate, ErrorMessage = Book.IsbnSchema)]
    public string Isbn { init; get; }

    [Required]
    [Range(Book.MinReleaseYear, int.MaxValue)]
    public int ReleaseYear { init; get; }

    [Required]
    public int? PublisherId { init; get; }

    [Required]
    public int? AuthorId { init; get; }

    public int? TypeId { init; get; }

    public IEnumerable<int> GenreIds { init; get; } = new List<int>();

    public string OriginalName { init; get; }

    public int? AgeLimitId { init; get; }

    public int? CoverArtId { init; get; }

    [RegularExpression(Book.FormatTemplate, ErrorMessage = Book.FormatSchema)]
    public string BookFormat { init; get; }

    [Range(1, int.MaxValue)]
    public int? PageQuantity { init; get; }

    #endregion
}