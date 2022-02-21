using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Books
{
    public class Tag : RelatedEntity
    {
        public ISet<BookTag> BookTag { set; get; }
    }
}
