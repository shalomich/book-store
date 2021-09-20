using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public class ProductAlbumIncludeRequirement<T> : IIncludeRequirement<T> where T : Product
    {
        public void Include(ref IQueryable<T> entities)
        {
            entities = entities
                .Include(product => product.Album)
                .ThenInclude(album => album.Images);
        }
    }
}
