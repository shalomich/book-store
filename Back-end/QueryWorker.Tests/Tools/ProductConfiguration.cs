
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Tests.Tools
{
    class ProductConfiguration : SortingConfiguration<Product>
    {
        public ProductConfiguration()
        {
            
            CreateSorting(product => product.Cost);
            CreateSorting(product => product.Name);
            
        }
    }
}
