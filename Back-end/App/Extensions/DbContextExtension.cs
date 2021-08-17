
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Entities;
using App;
using App.Entities.Publications;
using QueryWorker;
using QueryWorker.Args;

namespace App.Extensions
{
    public static class DbContextExtension
    {
        public static IQueryable<Entity> Entities(this ApplicationContext context, Type type, QueryArgs args) =>
        type.Name switch
        {
            nameof(Author) => context.QueryTransformer.Transform(context.Authors,args),
            nameof(Publisher) => context.QueryTransformer.Transform(context.Publishers, args),
            nameof(Genre) => context.QueryTransformer.Transform(context.Genres, args),
            nameof(PublicationType) => context.QueryTransformer.Transform(context.PublicationTypes, args),
            nameof(AgeLimit) => context.QueryTransformer.Transform(context.AgeLimits, args),
            nameof(CoverArt) => context.QueryTransformer.Transform(context.CoverArts, args),
            _ => Products(context, type, args)
        };
            
        public static IQueryable<Product> Products(this ApplicationContext context, Type type, QueryArgs args) =>
        type.Name switch
        {
            nameof(Publication) => context.QueryTransformer.Transform(context.Publications, args),
            _ => throw new ArgumentException()
        };    
    }
}
