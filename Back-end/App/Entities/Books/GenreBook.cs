using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Books
{
    public class GenreBook : IEntity
    {
        public int Id { set; get; }

        public virtual Book Book { set; get; }
        public int BookId { set; get; }

        public virtual Genre Genre { set; get; }
        public int GenreId { set; get; }

        public override bool Equals(object obj)
        {
            return obj is GenreBook book &&
                   BookId == book.BookId &&
                   GenreId == book.GenreId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookId, GenreId);
        }
    }
}
