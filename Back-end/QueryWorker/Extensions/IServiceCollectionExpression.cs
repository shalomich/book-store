using Microsoft.Extensions.DependencyInjection;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Extensions
{
    public static class IServiceCollectionExpression
    {
        public static IServiceCollection AddDataTransformer(this IServiceCollection services, Assembly currentAssembly)
        {
            services.AddSingleton(new ConfigurationFinder(currentAssembly));
            services.AddScoped<DataTransformerFacade>();

            return services;
        }
    }
}
