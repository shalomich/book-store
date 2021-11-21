using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Books
{
    public class AuthorSelectionOrder : IEntity
    {
        public int Id { set; get; }
        public int Number { set; get; }
        public Author Author { set; get; }
        public int AuthorId { set; get; }
    }
}
