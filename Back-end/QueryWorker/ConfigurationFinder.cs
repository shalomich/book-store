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

        public QueryConfiguration Find() 
        {
            var configuration = _assembly.GetTypes()
                .Single(type => type.IsSubclassOf(typeof(QueryConfiguration))
                    && type.IsAbstract == false);

            return Activator.CreateInstance(configuration) as QueryConfiguration;
        }
    }
}
