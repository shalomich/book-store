using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Publications
{
    public class GenrePublication
    {
        public int Id { set; get; }

        public virtual Publication Publication { set; get; }
        public int PublicationId { set; get; }

        public virtual Genre Genre { set; get; }
        public int GenreId { set; get; }

        public override bool Equals(object obj)
        {
            return obj is GenrePublication publication &&
                   PublicationId == publication.PublicationId &&
                   GenreId == publication.GenreId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PublicationId, GenreId);
        }
    }
}
