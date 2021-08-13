using QueryWorker.Configurations;
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

        public QueryNodeConfiguration<TClass> Find<TClass,TConfiguration>() where TClass : class 
            where TConfiguration : QueryNodeConfiguration<TClass>
        {
            var configuration = _assembly.GetTypes()
                .Single(type => type.BaseType == typeof(TConfiguration));

            return Activator.CreateInstance(configuration) as QueryNodeConfiguration<TClass>;
        }
    }
}
