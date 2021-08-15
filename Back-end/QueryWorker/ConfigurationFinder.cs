﻿using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker
{
    public class ConfigurationFinder
    {
        private readonly Assembly _assembly;
        public ConfigurationFinder(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        public QueryConfiguration<T> Find<T>() where T : class 
        {
            var configurationType = typeof(QueryConfiguration<>).MakeGenericType(typeof(T));
            var configuration = _assembly.GetTypes()
                .Single(type => type.IsSubclassOf(configurationType)
                    && type.IsAbstract == false);

            return Activator.CreateInstance(configuration) as QueryConfiguration<T>;
        }
    }
}
