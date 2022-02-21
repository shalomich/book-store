using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public record BookForm : ProductForm
    {
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

        public ISet<int> GenreIds { init; get; }

        public string OriginalName { init; get; }

        public int? AgeLimitId { init; get; }

        public int? CoverArtId { init; get; }

        [RegularExpression(Book.FormatTemplate, ErrorMessage = Book.FormatSchema)]
        public string BookFormat { init; get; }

        [Range(1,int.MaxValue)]
        public int? PageQuantity { init; get; }
        public ISet<int> TagIds { init; get; }

    }
}
