using Microsoft.Extensions.DependencyInjection;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDataTransformerBuildFacade(this IServiceCollection services, Assembly currentAssembly)
        {
            services.AddSingleton(new ConfigurationFinder(currentAssembly));
            services.AddScoped(typeof(DataTransformerBuildFacade<>));

            services.AddScoped(typeof(PaggingMetadataCollector<>));

            return services;
        }
    }
}
