using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public class ConfigurationFinder
    {
        private const string NotFoundConfigTemplateMesage = "Not found query configuration for type {0}";
        private readonly Assembly _assembly;
        public ConfigurationFinder(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        public QueryConfiguration<T> Find<T>() where T : class 
        {
            var type = typeof(T);
            var configurationType = typeof(QueryConfiguration<>).MakeGenericType(type);
           
            var configuration = _assembly.GetTypes()
                .SingleOrDefault(type => type.IsSubclassOf(configurationType)
                    && type.IsAbstract == false);

            if (configuration == null)
                throw new InvalidOperationException(string.Format(NotFoundConfigTemplateMesage, type.Name));

            return Activator.CreateInstance(configuration) as QueryConfiguration<T>;
        }
    }
}
