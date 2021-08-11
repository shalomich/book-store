
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
        public static IQueryable<Entity> Entities(this ApplicationContext context, Type type)
        {
            if (type.IsSubclassOf(typeof(Entity)) == false)
                throw new ArgumentException();
            
            return (IQueryable<Entity>) context
                .GetType()
                .GetMethods()
                .SingleOrDefault(method => method.Name == nameof(context.Set) 
                    && method.GetParameters().Count() == 0)
                .MakeGenericMethod(type)
                .Invoke(context, null);
        }
    }
}
