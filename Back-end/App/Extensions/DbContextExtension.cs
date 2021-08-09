
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Entities;
using App;
using App.Entities.Publications;

namespace App.Extensions
{
    public static class DbContextExtension
    {
        public static async Task<IEnumerable<Entity>> GetEntitiesAsync(this ApplicationContext context, Type entityType) => entityType.Name switch
        {
            nameof(Publication) => await context.Publications.ToListAsync(),
            nameof(Author) => await context.Authors.ToListAsync(),
            nameof(Publisher) => await context.Publishers.ToListAsync(),
            nameof(PublicationType) => await context.PublicationTypes.ToListAsync(),
            nameof(AgeLimit) => await context.AgeLimits.ToListAsync(),
            nameof(CoverArt) => await context.CoverArts.ToListAsync(),
            nameof(Genre) => await context.Genres.ToListAsync(),
            _ => throw new ArgumentException()
        };
    }
}
