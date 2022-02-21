using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Books
{
    public class BookTag : IEntity
    {
        public int Id { set; get; }

        public Book Book { set; get; }
        public int BookId { set; get; }

        public Tag Tag { set; get; }
        public int TagId { set; get; }

        public override bool Equals(object obj)
        {
            return obj is BookTag bookTag &&
                   BookId == bookTag.BookId &&
                   TagId == bookTag.TagId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookId, TagId);
        }
    }
}
