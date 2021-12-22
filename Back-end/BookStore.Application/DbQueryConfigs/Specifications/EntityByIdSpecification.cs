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
    internal class EntityByIdSpecification : Specification<IEntity>
    {
        private int Id { get; }

        public EntityByIdSpecification(int id)
        {
            Id = id;
        }

        public override Expression<Func<IEntity, bool>> ToExpression()
        {
            return entity => entity.Id == Id;
        }
    }
}
