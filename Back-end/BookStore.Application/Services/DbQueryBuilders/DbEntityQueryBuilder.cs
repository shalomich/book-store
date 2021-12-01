using Abp.Specifications;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.DbQueryConfigs.Orders;
using BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems;

namespace BookStore.Application.Services.DbQueryBuilders
{
    public class DbEntityQueryBuilder<T> : DbQueryBuilder<T> where T : class, IEntity
    {
        public DbEntityQueryBuilder(ApplicationContext context) : base(context)
        {
        }

        public DbEntityQueryBuilder<T> AddIncludeRequirements(params IIncludeRequirement<T>[] requirements)
        {
            foreach (var requirement in requirements)
                AddBuildItem(new IncludeBuildItem<T>(requirement));

            return this;
        }

        public DbEntityQueryBuilder<T> AddSpecification(ISpecification<T> specification)
        {
            AddBuildItem(new SpecificationBuildItem<T>(specification));

            return this;
        }

        public DbEntityQueryBuilder<T> AddOrder(IOrder<T> order)
        {
            AddBuildItem(new OrderBuildItem<T>(order));

            return this;
        }
    }
}
