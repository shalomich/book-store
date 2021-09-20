using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public abstract class DbQueryBuilder<T> : IDbQueryBuilder<T> where T : class, IEntity
    {
        private ApplicationContext Context { get; }
        private Queue<IQueryBuildItem<T>> BuildItems { get; }

        public DbQueryBuilder(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            BuildItems = new Queue<IQueryBuildItem<T>>();
        }

        protected void AddBuildItem(IQueryBuildItem<T> buildItem) 
        {
            if (buildItem != null)
                BuildItems.Enqueue(buildItem);
        }
            

        public IQueryable<T> Build()
        {
            var entities = Context.Set<T>().AsQueryable();

            while (BuildItems.TryDequeue(out IQueryBuildItem<T> buildItem))
                buildItem.AddQuery(ref entities);

            return entities;
        }
    }
}
