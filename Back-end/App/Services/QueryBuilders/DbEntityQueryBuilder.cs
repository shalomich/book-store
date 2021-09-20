using App.Entities;
using App.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
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
