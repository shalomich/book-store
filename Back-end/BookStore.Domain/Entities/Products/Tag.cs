using System.Collections.Generic;


namespace BookStore.Domain.Entities.Products
{
    public class Tag : RelatedEntity
    {
        public TagGroup TagGroup { set; get; }
        public int? TagGroupId { set; get; }
        public ISet<ProductTag> ProductTags { set; get; }
        public ISet<User> Users { set; get; }
    }
}
