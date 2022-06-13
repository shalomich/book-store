using System.Collections.Generic;

namespace BookStore.Domain.Entities.Products;
public class Tag : RelatedEntity
{
    public TagGroup TagGroup { set; get; }
    public int? TagGroupId { set; get; }
    public IEnumerable<ProductTag> ProductTags { set; get; } = new List<ProductTag>();
    public IEnumerable<User> Users { set; get; } = new List<User>();
}

