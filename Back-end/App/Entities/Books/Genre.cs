using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Books
{
    public class Genre : FormEntity
    {
        public virtual ISet<GenreBook> Books { set; get; } 
    }
}
