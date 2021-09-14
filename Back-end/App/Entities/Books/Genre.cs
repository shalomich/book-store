using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Books
{
    public class Genre : RelatedEntity
    {
        public ISet<GenreBook> Books { set; get; } 
    }
}
