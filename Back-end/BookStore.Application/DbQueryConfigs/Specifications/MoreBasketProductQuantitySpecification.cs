using Abp.Specifications;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    internal class MoreBasketProductQuantitySpecification : Specification<BasketProduct>
    {
        public override Expression<Func<BasketProduct, bool>> ToExpression()
        {
            return basketProduct => basketProduct.Quantity > basketProduct.Product.Quantity;
        }
    }
}
