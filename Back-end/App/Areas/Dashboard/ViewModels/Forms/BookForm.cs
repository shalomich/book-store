using App.Attributes.FormModel;
using App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    [FormModel]
    public record BookForm : ProductForm
    {
        [Required]
        [RegularExpression(Book.IsbnTemplate, ErrorMessage = Book.IsbnSchema)]
        public string Isbn { init; get; }

        [Required]
        [Range(Book.MinReleaseYear, int.MaxValue)]
        [FormField(FormFieldType.Number, "Год издания")]
        public int ReleaseYear { init; get; }

        [Required]
        [FormField(FormFieldType.Select, "Издатель")]
        public int? PublisherId { init; get; }

        [Required]
        [FormField(FormFieldType.Select, "Автор произведения")]
        public int? AuthorId { init; get; }

        [FormField(FormFieldType.Select, "Тип произведения",false)]
        public int? TypeId { init; get; }

        [FormField(FormFieldType.SelectMultiple, "Жанры",false)]
        public ISet<int> GenreIds { init; get; }

        [FormField(FormFieldType.Text, "Оригинальное название",false)]
        public string OriginalName { init; get; }

        [FormField(FormFieldType.Select, "Возрастное ограничение",false)]
        public int? AgeLimitId { init; get; }

        [FormField(FormFieldType.Select, "Тип обложки", false)]
        public int? CoverArtId { init; get; }

        [RegularExpression(Book.FormatTemplate, ErrorMessage = Book.FormatSchema)]
        [FormField(FormFieldType.Text, "Размеры", false)]
        public string BookFormat { init; get; }

        [Range(1,int.MaxValue)]
        [FormField(FormFieldType.Number, "Количество страниц", false)]
        public int? PageQuantity { init; get; }

    }
}
