using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Storage.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> entities) where T : Entity => typeof(T).Name switch
        {
            "Publication" => entities.Include("Author").Include("Publisher"),
            "Publisher" => entities.Include("Publications"),
            "Author" => entities.Include("Publications")
        };
    }
}
