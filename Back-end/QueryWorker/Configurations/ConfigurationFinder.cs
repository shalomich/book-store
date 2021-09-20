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
            var objectType = typeof(T);

            Type configurationType = null;

            if ((configurationType = FindConfigurationType(objectType)) == null
                && (configurationType = FindGenericConfigurationType(objectType)) == null)
                throw new InvalidOperationException(string.Format(NotFoundConfigTemplateMesage, objectType.Name));

            return Activator.CreateInstance(configurationType) as QueryConfiguration<T>;
        }

        private Type FindGenericConfigurationType(Type objectType)
        {
            var configurationBaseType = typeof(QueryConfiguration<>).MakeGenericType(objectType);

            var configurationTypeCandidates = _assembly.GetTypes()
                .Where(type => type.IsGenericType && type.IsAbstract == false);

            Type configurationType = null;

            foreach(var configurationTypeCandidate in configurationTypeCandidates)
            {
                try
                {
                    configurationType = configurationTypeCandidate.GetGenericTypeDefinition()
                        .MakeGenericType(objectType);
                }
                catch (Exception)
                {
                    continue;
                }

                if (configurationType.IsSubclassOf(configurationBaseType))
                    return configurationType;
            }

            return null;
        }
        private Type FindConfigurationType(Type objectType)
        {
            var configurationBaseType = typeof(QueryConfiguration<>).MakeGenericType(objectType);

            return _assembly.GetTypes()
                .SingleOrDefault(type => type.IsSubclassOf(configurationBaseType)
                        && type.IsAbstract == false); 
        }
    }
}
