using App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record PublicationForm : ProductForm
    {
        [Required]
        [RegularExpression(Publication.IsbnTemplate)]
        public string Isbn { init; get; }

        [Required]
        [Range(Publication.MinReleaseYear, int.MaxValue)]
        public int ReleaseYear { init; get; }

        [Required]
        public int PublisherId { init; get; }

        [Required]
        public int AuthorId { init; get; }

        public int? TypeId { init; get; }

        public ISet<int> GenreIds { init; get; }

        public string OriginalName { init; get; }
        public int? AgeLimitId { init; get; }
        public int? CoverArtId { init; get; }

        [RegularExpression(Publication.FormatTemplate)]
        public string PublicationFormat { init; get; }

        public int? PageQuantity { init; get; }

    }
}
