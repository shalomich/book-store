using Microsoft.Extensions.Configuration;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Services
{
    public class EntityConfig<T> where T : Entity
    {
        private const string _entityConfigSectionName = "entityConstants";
        private IConfigurationSection _entityConfigSection;

        public EntityConfig(IConfiguration configuration)
        {
            _entityConfigSection = configuration.GetSection(_entityConfigSectionName);
        }

        public virtual Dictionary<string,object> GetConstants()
        {
            string entityName = typeof(T).Name;

            IConfigurationSection currentConfigSection = _entityConfigSection.GetSection(entityName);

            Dictionary<string, object> options = currentConfigSection
                .GetSection("options")
                .GetChildren()
                .ToDictionary(section => section.Key, section => 
                {
                    var options = section.Get<string[]>();
                    return (object) options.Select(option => KeyValuePair.Create(option, option)).ToList();
                });
            Dictionary<string, object> numbers = currentConfigSection
                .GetSection("numbers")
                .GetChildren()
                .ToDictionary(section => section.Key, section => (object)section.Get<int>());
            Dictionary<string, object> strings = currentConfigSection
                .GetSection("strings")
                .GetChildren()
                .ToDictionary(section => section.Key, section => (object)section.Get<string>());

            return options
                .Concat(numbers)
                .Concat(strings)
                .ToDictionary(property => property.Key, property => property.Value);
        }
    }
}
