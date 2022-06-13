using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using System.Collections.Generic;

namespace BookStore.Persistance.Data;
internal class DefaultBookRelatedEntities
{
    public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    public IEnumerable<BookType> BookTypes { get; set; } = new List<BookType>();
    public IEnumerable<AgeLimit> AgeLimits { get; set; } = new List<AgeLimit>();
    public IEnumerable<CoverArt> CoverArts { get; set; } = new List<CoverArt>();
    public IEnumerable<TagGroup> TagGroups { get; set; } = new List<TagGroup>();
}

