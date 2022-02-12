using BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services.DbQueryBuilders
{
    public abstract class DbQueryBuilder<T> : IDbQueryBuilder<T> where T : class, IEntity
    {
        private ApplicationContext Context { get; }
        private List<IQueryBuildItem<T>> BuildItems { get; }

        public DbQueryBuilder(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            BuildItems = new List<IQueryBuildItem<T>>();
        }

        protected void AddBuildItem(IQueryBuildItem<T> buildItem) 
        {
            if (buildItem != null)
                BuildItems.Add(buildItem);
        }
            

        public IQueryable<T> Build()
        {
            var entities = Context.Set<T>().AsQueryable();

            foreach (var buildItem in BuildItems)
                buildItem.AddQuery(ref entities);

            return entities;
        }

        public void Reset()
        {
            BuildItems.Clear();
        }
    }
}
