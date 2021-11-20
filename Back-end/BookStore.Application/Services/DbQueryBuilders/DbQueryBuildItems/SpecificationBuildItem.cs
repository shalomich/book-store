using Abp.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems
{
    internal class SpecificationBuildItem<T> : IQueryBuildItem<T> where T : IEntity
    {
        private ISpecification<T> Specification { get; }

        public SpecificationBuildItem(ISpecification<T> specification)
        {
            Specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public void AddQuery(ref IQueryable<T> entities)
        {
            entities = entities.Where(Specification.ToExpression());
        }
    }
}
