using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QueryConfigs
{
    public abstract class ProductQueryConfig<T> : EntityQueryConfig<T> where T : Product
    {
        protected ProductQueryConfig()
        {
            CreateSorting("cost", book => book.Cost);
            CreateSorting("addingDate", book => book.AddingDate);

            CreateFilter("cost", book => book.Cost);
            CreateFilter("quantity", book => book.Quantity);
        }
    }
}
