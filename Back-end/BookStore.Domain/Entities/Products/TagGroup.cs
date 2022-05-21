using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Entities.Products;
public class TagGroup : RelatedEntity
{
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
}

