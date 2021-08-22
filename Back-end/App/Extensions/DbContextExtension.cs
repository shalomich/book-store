﻿
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
        public static IQueryable<FormEntity> FormEntities(this ApplicationContext context, Type type, QueryArgs args) =>
        type.Name switch
        {
            nameof(Author) => context.DataTransformer.Transform(context.Authors,args),
            nameof(Publisher) => context.DataTransformer.Transform(context.Publishers, args),
            nameof(Genre) => context.DataTransformer.Transform(context.Genres, args),
            nameof(PublicationType) => context.DataTransformer.Transform(context.PublicationTypes, args),
            nameof(AgeLimit) => context.DataTransformer.Transform(context.AgeLimits, args),
            nameof(CoverArt) => context.DataTransformer.Transform(context.CoverArts, args),
            _ => Products(context, type, args)
        };
            
        public static IQueryable<Product> Products(this ApplicationContext context, Type type, QueryArgs args) =>
        type.Name switch
        {
            nameof(Publication) => context.DataTransformer.Transform(context.Publications, args),
            _ => throw new ArgumentException()
        };

        public static IQueryable<IEntity> Entities(this ApplicationContext context, Type type, PaggingArgs args)
        {
            if (typeof(IEntity).IsAssignableFrom(type) == false)
                throw new ArgumentException();

            var entities = (IQueryable<IEntity>) context
                .GetType()
                .GetMethods()
                .SingleOrDefault(method => method.Name == nameof(context.Set)
                    && method.GetParameters().Count() == 0)
                .MakeGenericMethod(type)
                .Invoke(context, null);

            return context.DataTransformer.Transform(entities, args);
        }
    }
}
