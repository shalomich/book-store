﻿using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems
{
    internal class IncludeBuildItem<T> : IQueryBuildItem<T> where T : IEntity
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
