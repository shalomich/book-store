using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QueryConfigs
{
    public abstract class ProductQueryConfig<T> : QueryConfiguration<T> where T : Product
    {
        protected ProductQueryConfig()
        {
            CreateSorting("name", entity => entity.Name);
            CreateSorting("cost", book => book.Cost);
            CreateSorting("addingDate", book => book.AddingDate);

            CreateFilter("name", entity => entity.Name);
            CreateFilter("cost", book => book.Cost);
            CreateFilter("quantity", book => book.Quantity);
            
            CreateSearch("name", entity => entity.Name);
        }
    }
}
