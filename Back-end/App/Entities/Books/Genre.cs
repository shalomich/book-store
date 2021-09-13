using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Books
{
    public class Genre : RelatedEntity
    {
        public virtual ISet<GenreBook> Books { set; get; } 
    }
}
