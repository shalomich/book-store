using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class Tag : RelatedEntity
    {
        public ISet<ProductTag> ProductTags { set; get; }
    }
}
