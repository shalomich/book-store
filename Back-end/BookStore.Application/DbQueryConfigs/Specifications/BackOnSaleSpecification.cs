using Abp.Specifications;
using BookStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    internal class BackOnSaleSpecification<T> : Specification<T> where T : Product
    {
        private const int BackOnSaleDaysCount = 7;
        public override Expression<Func<T, bool>> ToExpression()
        {
            return product => product.ProductCloseout != null
                && product.ProductCloseout.ReplenishmentDate != null
                && EF.Functions.DateDiffDay(product.ProductCloseout.ReplenishmentDate, DateTime.Now) < BackOnSaleDaysCount; ;
        }
    }
}
