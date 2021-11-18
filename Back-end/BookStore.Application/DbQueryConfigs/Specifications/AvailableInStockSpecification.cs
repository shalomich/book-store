using Abp.Specifications;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    internal class AvailableInStockSpecification<T> : Specification<T> where T : Product
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return product => product.Quantity != 0;
        }
    }
}
