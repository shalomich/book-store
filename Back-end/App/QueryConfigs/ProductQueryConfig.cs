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
            CreateSorting("cost", publication => publication.Cost);
            CreateSorting("addingDate", publication => publication.AddingDate);

            CreateFilter("cost", publication => publication.Cost);
            CreateFilter("quantity", publication => publication.Quantity);
        }
    }
}
