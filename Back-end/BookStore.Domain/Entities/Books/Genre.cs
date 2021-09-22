using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Books
{
    public class Genre : RelatedEntity
    {
        public ISet<GenreBook> Books { set; get; } 
    }
}
