using Abp.Specifications;
using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    public class BasketProductByUserIdSpecification : Specification<BasketProduct>
    {
        private int UserId { get; }

        public BasketProductByUserIdSpecification(int userId)
        {
            UserId = userId;
        }

        public override Expression<Func<BasketProduct, bool>> ToExpression()
        {
            return basketProduct => basketProduct.UserId == UserId;
        }
    }
}
