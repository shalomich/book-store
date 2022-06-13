
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Entities.Books;
public class Book : Product
{
    public const int MinReleaseYear = 2000;
    public static readonly int MaxReleaseYear = DateTime.Today.Year;

    public const string IsbnTemplate = @"^978-5-\d{6}-\d{2}-\d{1}$";
    public const string FormatTemplate = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";

    public const string IsbnSchema = "978-5-XXXXXX-XX-X";       
    public const string FormatSchema = "[10-99]x[10-99]x[1-9]";

    public string Isbn { set; get; }
    public int ReleaseYear { set; get; }
    public string OriginalName { set; get; }
    public string BookFormat { set; get; }
    public int? PageQuantity { set; get; }

    public Publisher Publisher { set; get; }
    public int PublisherId { set; get; }

    public Author Author { set; get; }
    public int AuthorId { set; get; }

    public BookType Type { set; get; }
    public int? TypeId { set; get; }

    public AgeLimit AgeLimit { set; get; }
    public int? AgeLimitId { set; get; }

    public CoverArt CoverArt { set; get; }
    public int? CoverArtId { set; get; }

    public IEnumerable<GenreBook> GenresBooks { set; get; } = new List<GenreBook>();
    public IEnumerable<View> Views { get; set; } = new List<View>();
    public IEnumerable<BattleBook> BattleBooks { get; set; } = new List<BattleBook>();

    public ISet<string> Tags => ProductTags
        ?.Select(bookTag => bookTag.Tag.Name)
        .ToHashSet();
}
