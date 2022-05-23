using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Entities.Products;
public class TagGroup : RelatedEntity
{
    public const string ColorHexMask = @"^#[A-Fa-f0-9]{6}$";
    public string ColorHex { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
}

