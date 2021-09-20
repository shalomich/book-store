using App.Entities;
using App.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public class IncludeBuildItem<T> : IQueryBuildItem<T> where T : IEntity
    {
        private IIncludeRequirement<T> Requirement { get; }

        public IncludeBuildItem(IIncludeRequirement<T> requirement)
        {
            Requirement = requirement ?? throw new ArgumentNullException(nameof(requirement));
        }

        public void AddQuery(ref IQueryable<T> entities)
        {
            Requirement.Include(ref entities);
        }
    }
}
