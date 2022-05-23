using BookStore.Domain.Entities;
using BookStore.Persistance;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
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
    }
}
