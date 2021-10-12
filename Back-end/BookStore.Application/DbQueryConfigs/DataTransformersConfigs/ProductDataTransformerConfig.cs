using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.DataTransformersConfigs
{
    public abstract class ProductDataTransformerConfig<T> : QueryConfiguration<T> where T : Product
    {
        protected ProductDataTransformerConfig()
        {
            CreateSorting(nameof(Product.Name), entity => entity.Name);
            CreateSorting(nameof(Product.Cost), book => book.Cost);
            CreateSorting(nameof(Product.AddingDate), book => book.AddingDate);

            CreateFilter(nameof(Product.Name), entity => entity.Name);
            CreateFilter(nameof(Product.Cost), book => book.Cost);
            CreateFilter(nameof(Product.Quantity), book => book.Quantity);
            
            CreateSearch(nameof(Product.Name), entity => entity.Name);
        }
    }
}
